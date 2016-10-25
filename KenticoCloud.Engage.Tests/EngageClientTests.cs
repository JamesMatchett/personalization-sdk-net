using System;
using System.Threading.Tasks;
using Xunit;

namespace KenticoCloud.Engage.Tests
{
    //TODO: Have some testing data on production server which will not change and verify them here.
    public class EngageClientTests
    {
        private readonly string _uid;
        private readonly string _sid;
        private readonly EngageClient _client;

        public EngageClientTests()
        {
            _uid = "49d505aaa8316229";
            _sid = "93786ec335de0444";
            var apiKey = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJPbmxpbmUgSldUIEJ1aWxkZXIiLCJpYXQiOjE0NzczNzc0MTQsImV4cCI6MTUwODkxMzQxNCwiYXVkIjoid3d3LmV4YW1wbGUuY29tIiwic3ViIjoianJvY2tldEBleGFtcGxlLmNvbSIsImRuIjoibG9jYWxob3N0In0.FqFOXw38U7ka4RN58YhXEjWqalCvzTA43ZsWGSnsVTQ";
            var apiUri = "http://localhost:34245";
            _client = new EngageClient(apiUri, apiKey);
        }

        [Fact]
        public async Task GetVisitorUsualLocationAsync()
        {
            var r = await _client.GetVisitorUsualLocationAsync(_uid);
        }

        [Fact]
        public async Task GetCurrentLocationAsync()
        {
            var r = await _client.GetCurrentLocationAsync(_uid, _sid);
        }

        [Fact]
        public async Task GetFirstVisitAsync()
        {
            var r = await _client.GetFirstVisitAsync(_uid);
        }

        [Fact]
        public async Task GetLastVisitAsync()
        {
            var r = await _client.GetLastVisitAsync(_uid);
        }

        [Fact]
        public async Task GetActivitySummaryAsync()
        {
            var r = await _client.GetActivitySummaryAsync(_uid);
        }

        [Fact]
        public async Task GetCurrentSessionAsync()
        {
            var r = await _client.GetCurrentSessionAsync(_uid, _sid);
        }

        [Fact]
        public async Task GetVisitorActionsAsync()
        {
            var r = await _client.GetVisitorActionsAsync(_uid, ActionTypeEnum.PageVisit);
        }

        [Fact]
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
