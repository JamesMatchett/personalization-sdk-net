using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace KenticoCloud.Personalization.Tests
{
    public class PersonalizationClientTests
    {
        private readonly string _uid;
        private readonly string _sid;
        private readonly PersonalizationClient _client;

        public PersonalizationClientTests()
        {
            _uid = "88-9a2c-cdefb89a46ef";
            _sid = "0d-a495-89bb29e6c18a";
            var apiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1aWQiOiJ1c3JfMHZRWm12OHFYcUpmSWhzZmRUVW1FViIsInBpZCI6IjQ2MjUxN2NlLTlkYmYtNDRmMC1hNTdiLTIyYjlkNTA3NDdmZCIsImp0aSI6Ing5TjVURGJqd1BCdUZ5bXIiLCJhdWQiOiJlbmdhZ2UtYXBpLmtlbnRpY29jbG91ZC5jb20ifQ.IvaiOpLYs-UW54l2sagxkDH6VwynZX8G4D4Yx-wYTjw";
            _client = new PersonalizationClient(apiKey);
        }

        [Test]
        public async Task GetVisitorUsualLocationAsync()
        {
            var r = await _client.GetVisitorUsualLocationAsync(_uid);
            Assert.AreEqual("Jiaoziya", r.City);
            Assert.AreEqual("China", r.Country);
            Assert.AreEqual("", r.State);
        }

        [Test]
        public async Task GetCurrentLocationAsync()
        {
            var r = await _client.GetCurrentLocationAsync(_uid, _sid);
            Assert.AreEqual("Jiaoziya", r.City);
            Assert.AreEqual("China", r.Country);
            Assert.AreEqual("", r.State);
        }

        [Test]
        public async Task GetFirstVisitAsync()
        {
            var r = await _client.GetFirstVisitAsync(_uid);
            Assert.AreEqual("http://reddit.com", r.Origin);
            Assert.AreEqual(DateTimeOffset.Parse("2017-07-17 12:53:07+00:00"), r.VisitDateTime);
        }

        [Test]
        public async Task GetLastVisitAsync()
        {
            var r = await _client.GetLastVisitAsync(_uid);
            Assert.AreEqual("http://reddit.com", r.Origin);
            Assert.AreEqual(DateTimeOffset.Parse("2017-07-17 12:53:07+00:00"), r.VisitDateTime);
        }

        [Test]
        public async Task GetActivitySummaryAsync()
        {
            var r = await _client.GetActivitySummaryAsync(_uid);
            Assert.AreEqual(TimeSpan.Zero, r.ActivityTimeSpan);
            Assert.AreEqual(2, r.AverageActionsInSession);
            Assert.AreEqual(1, r.TotalSessions);
        }

        [Test]
        public async Task GetCurrentSessionAsync()
        {
            var r = await _client.GetCurrentSessionAsync(_uid, _sid);
            Assert.AreEqual(DateTimeOffset.Parse("2017-07-17 12:53:07+00:00"), r.Started);
            Assert.AreEqual("http://reddit.com", r.Origin);
            Assert.AreEqual(2, r.Actions.Count);
            Assert.AreEqual(ActionTypeEnum.PageVisit, r.Actions[0].ActionType);
            Assert.AreEqual("/articles/coffee_beverages_explained", r.Actions[0].PageUrl);
            Assert.AreEqual(ActionTypeEnum.FormSubmit, r.Actions[1].ActionType);
            Assert.AreEqual("/products/brazil_natural_barra_grande", r.Actions[1].PageUrl);
        }

        [Test]
        public async Task GetVisitorActionsAsync()
        {
            var r = await _client.GetVisitorActionsAsync(_uid, ActionTypeEnum.PageVisit);
            Assert.AreEqual(DateTimeOffset.Parse("2017-07-17 12:54:07+00:00"), r.LastOccurence);
            Assert.AreEqual(true, r.Activity);
        }

        [Test]
        public async Task GetVisitorActionsAsync_withDetails()
        {
            var r = await _client.GetVisitorActionsAsync(_uid, ActionTypeEnum.Session, new ActionDetails()
            {
                DaysAgo = 2,
                CustomActionName = "MyAction",
                PageUrl = "/"
            });
            Assert.AreEqual(null, r.LastOccurence);
            Assert.AreEqual(false, r.Activity);
        }

        [Test]
        public async Task GetVisitorSegmentsAsync()
        {
            var r = await _client.GetVisitorSegmentsAsync(_uid);
            Assert.AreEqual(0, r.Segments.Length);
        }
    }
}
