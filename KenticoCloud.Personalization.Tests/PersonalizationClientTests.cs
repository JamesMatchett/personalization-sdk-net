using System.Threading.Tasks;
using NUnit.Framework;

namespace KenticoCloud.Personalization.Tests
{
    //TODO: Have some testing data on production server which will not change and verify them here.
    public class PersonalizationClientTests
    {
        private readonly string _uid;
        private readonly string _sid;
        private readonly PersonalizationClient _client;

        public PersonalizationClientTests()
        {
            _uid = "";
            _sid = "";
            var apiKey = "";
            var apiUri = "";
            _client = new PersonalizationClient(apiUri, apiKey);
        }

        [Test]
        public async Task GetVisitorUsualLocationAsync()
        {
            var r = await _client.GetVisitorUsualLocationAsync(_uid);
        }

        [Test]
        public async Task GetCurrentLocationAsync()
        {
            var r = await _client.GetCurrentLocationAsync(_uid, _sid);
        }

        [Test]
        public async Task GetFirstVisitAsync()
        {
            var r = await _client.GetFirstVisitAsync(_uid);
        }

        [Test]
        public async Task GetLastVisitAsync()
        {
            var r = await _client.GetLastVisitAsync(_uid);
        }

        [Test]
        public async Task GetActivitySummaryAsync()
        {
            var r = await _client.GetActivitySummaryAsync(_uid);
        }

        [Test]
        public async Task GetCurrentSessionAsync()
        {
            var r = await _client.GetCurrentSessionAsync(_uid, _sid);
        }

        [Test]
        public async Task GetVisitorActionsAsync()
        {
            var r = await _client.GetVisitorActionsAsync(_uid, ActionTypeEnum.PageVisit);
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
        }
    }
}
