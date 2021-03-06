﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KenticoCloud.Personalization
{
    /// <summary>
    /// Class for tracking visitors and activities in Kentico cloud using the tracker API.
    /// </summary>
    public class TrackingClient
    {
        private readonly Guid _projectId;
        private readonly HttpClient _httpClient;
        private readonly RandomIdGenerator _randomIdGenerator = new RandomIdGenerator();

        private const string VisitorApiRoutePrefix = "v3/track";
        private const int UID_MAX_LENGTH = 20;

        /// <summary>
        /// Client constructor for production API.
        /// </summary>
        /// <param name="projectId">ID of your project, can be found in the Kentico cloud application</param>
        public TrackingClient(Guid projectId) : this("https://engage-ket.kenticocloud.com", projectId)
        {
        }

        /// <summary>
        /// Client constructor for production API.
        /// </summary>
        /// <param name="endpointUri"></param>
        /// <param name="projectId"></param>
        public TrackingClient(string endpointUri, Guid projectId)
        {
            _projectId = projectId;

            _httpClient = new HttpClient { BaseAddress = new Uri(endpointUri) };
        }

        /// <summary>
        /// Records start of a new session for the visitor and returns ID of the recorded session.
        /// </summary>
        /// <param name="uid">ID of the tracked visitor</param>
        /// <returns>ID of the created session</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="uid"/> is invalid.</exception>
        /// <exception cref="PersonalizationException">Thrown when request to the Kentico cloud server wasn't successful.</exception>
        public async Task<string> RecordNewSession(string uid)
        {
            ValidateUid(uid, nameof(uid));

            string sid = _randomIdGenerator.Generate();

            var postContent =new Dictionary<string, string>
            {
                { "uid", uid },
                { "sid", sid }
            };

            await PerformTrackingRequestAsync($"{VisitorApiRoutePrefix}/{_projectId}/session", postContent);

            return sid;
        }

        /// <summary>
        /// Records custom activity for the visitor in the given session. 
        /// Activity of the given <paramref name="activityCodename"/> must be defined in Kentico Cloud application otherwise activity won't be logged.
        /// </summary>
        /// <param name="uid">ID of the tracked visitor</param>
        /// <param name="sid">ID of the session this activity belongs to</param>
        /// <param name="activityCodename">Name of the activity</param>
        /// <exception cref="ArgumentException">Thrown when some of the arguments are invalid.</exception>
        /// <exception cref="PersonalizationException">Thrown when request to the Kentico cloud server wasn't successful.</exception>
        public async Task RecordActivity(string uid, string sid, string activityCodename)
        {
            ValidateUid(uid, nameof(uid));
            ValidateSid(sid, nameof(sid));
            if (string.IsNullOrEmpty(activityCodename))
            {
                throw new ArgumentException("Name of the activity must be set", nameof(activityCodename));
            }

            var postContent = new Dictionary<string, string>
            {
                { "uid", uid },
                { "sid", sid },
                { "codename", activityCodename }
            };

            await PerformTrackingRequestAsync($"{VisitorApiRoutePrefix}/{_projectId}/activity", postContent);
        }

        /// <summary>
        /// Records visitor's information to its contact profile.
        /// </summary>
        /// <param name="uid">ID of the tracked visitor</param>
        /// <param name="sid">ID of the session this activity belongs to</param>
        /// <param name="contact">Visitor's information</param>
        /// <returns>Status code for the performed request</returns>
        /// <exception cref="ArgumentException">Thrown when some of the arguments are invalid.</exception>
        /// <exception cref="PersonalizationException">Thrown when request to the Kentico cloud server wasn't successful.</exception>
        public async Task<HttpStatusCode> RecordVisitor(string uid, string sid, Contact contact)
        {
            ValidateUid(uid, nameof(uid));
            ValidateSid(sid, nameof(sid));
            if (string.IsNullOrEmpty(contact.Email))
            {
                throw new ArgumentException("Email must be set", nameof(contact));
            }

            var postContent = new Dictionary<string, string>
            {
                { "uid", uid },
                { "sid", sid },
                { "email", contact.Email },
                { "company", contact.Company },
                { "name", contact.Name },
                { "phone", contact.Phone },
                { "website", contact.Website },
            };

            return await PerformTrackingRequestAsync($"{VisitorApiRoutePrefix}/{_projectId}/contact", postContent);
        }

        private async Task<HttpStatusCode> PerformTrackingRequestAsync(string url, Dictionary<string, string> requestContent)
        {
            using (var response = await _httpClient.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(requestContent), Encoding.UTF8, "application/json")))
            {
                var responseBody = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new PersonalizationException(response.StatusCode, responseBody);
                }

                return response.StatusCode;
            }
        }

        private void ValidateSid(string value, string name)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException($"Parameter {name} must be set.", name);
            }

            if (!Regex.IsMatch(value, @"^[a-zA-Z0-9]{16}$"))
            {
                throw new ArgumentException($"Format of the {name} parameter is invalid.");
            }
        }
        
        private void ValidateUid(string value, string name)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException($"Parameter {name} must be set.", name);
            }

            if (value.Length > UID_MAX_LENGTH)
            {
                throw new ArgumentException($"Format of the {name} parameter is invalid. Should have maximum length of {UID_MAX_LENGTH}");
            }
        }
    }
}
