using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace KenticoCloud.Personalization.Tests
{
    [TestFixture]
    public class TrackingClientTests
    {
        private const string TEST_UID = "462517ce9dbf44f0";
        private const string TEST_ACTIVITY_NAME = "SDKTestActivity";
        private TrackingClient _client;

        [SetUp]
        public void SetUp()
        {
            _client = new TrackingClient(new Guid("462517ce-9dbf-44f0-a57b-22b9d50747fd"));
        }

        [TestCase(null)]
        [TestCase("abcde12345678$|a")]
        public void RecordNewSession_ThrowsForIncorrectUid(string uid)
        {
            Assert.ThrowsAsync<ArgumentException>(async () => await _client.RecordNewSession(uid));
        }

        [Test]
        public async Task RecordNewSession_CorrectSessionIdIsReturned()
        {
            string sid = await _client.RecordNewSession(TEST_UID);
            StringAssert.IsMatch("^[A-Za-z0-9]{16}$", sid);
        }

        [TestCase(null)]
        [TestCase("abcde12345678$|a")]
        public void RecordActivity_ThrowsForIncorrectUid(string uid)
        {
            string validSid = "562517ce9dbf44f0";
            Assert.ThrowsAsync<ArgumentException>(async () => await _client.RecordActivity(uid, validSid, TEST_ACTIVITY_NAME));
        }

        [TestCase(null)]
        [TestCase("abcde12345678$|a")]
        public void RecordActivity_ThrowsForIncorrectSid(string sid)
        {
            Assert.ThrowsAsync<ArgumentException>(async () => await _client.RecordActivity(TEST_UID, sid, TEST_ACTIVITY_NAME));
        }
        
        [TestCase(null)]
        [TestCase("")]
        public void RecordActivity_ThrowsForBadActivityName(string activityName)
        {
            string validSid = "562517ce9dbf44f0";
            Assert.ThrowsAsync<ArgumentException>(async () => await _client.RecordActivity(TEST_UID, validSid, activityName));
        }

        [Test]
        public async Task RecordActivity_LogsWithoutThrowingException()
        {
            string sid = await _client.RecordNewSession(TEST_UID);
            Assert.DoesNotThrowAsync(async () => await _client.RecordActivity(TEST_UID, sid, TEST_ACTIVITY_NAME));
        }
    }
}
