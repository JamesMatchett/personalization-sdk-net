using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;

namespace KenticoCloud.Personalization.Tests
{
    [TestFixture]
    public class PersonalizationClientTests
    {
        private readonly string _uid;
        private readonly string _sid;
        private readonly PersonalizationClient _client;

        public PersonalizationClientTests()
        {
            _uid = "9a2ccdefb89a46ef";
            _sid = "a49589bb29e6c18a";
            var projectId = new Guid("462517ce-9dbf-44f0-a57b-22b9d50747fd");
            var apiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1aWQiOiJ1c3JfMHZRWm12OHFYcUpmSWhzZmRUVW1FViIsInBpZCI6IjQ2MjUxN2NlLTlkYmYtNDRmMC1hNTdiLTIyYjlkNTA3NDdmZCIsImp0aSI6Ing5TjVURGJqd1BCdUZ5bXIiLCJhdWQiOiJlbmdhZ2UtYXBpLmtlbnRpY29jbG91ZC5jb20ifQ.IvaiOpLYs-UW54l2sagxkDH6VwynZX8G4D4Yx-wYTjw";
            _client = new PersonalizationClient(apiKey, projectId);
        }

        [Test]
        public async Task GetVisitorSegmentsAsync()
        {
            var r = await _client.GetVisitorSegmentsAsync(_uid);
            Assert.AreEqual(1, r.Segments.Length);
        }

        [Test]
        public async Task GetVisitorsInSegmentAsync()
        {
            var r = await _client.GetVisitorsInSegmentAsync("customers_who_requested_a_coffee_sample");
            Assert.IsTrue(r.Count > 0);
        }
    }
}
