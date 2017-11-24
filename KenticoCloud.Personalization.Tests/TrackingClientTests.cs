using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace KenticoCloud.Personalization.Tests
{
    public class TrackingClientTests
    {
        private TrackingClient _client;

        [SetUp]
        public void SetUp()
        {
            _client = new TrackingClient(new Guid("462517ce-9dbf-44f0-a57b-22b9d50747fd"));
        }

        [TestCase(null)]
        [TestCase("asdf")]
        [TestCase("abcde12345678$|a")]
        public void RecordNewSession_ThrowsForIncorrectUid(string uid)
        {
            Assert.ThrowsAsync<ArgumentException>(async () => await _client.RecordNewSession(uid));
        }

        [Test]
        public async Task RecordNewSession_CorrectSessionIdIsReturned()
        {
            string sid = await _client.RecordNewSession("462517ce9dbf44f0");
            StringAssert.IsMatch("^[A-Za-z0-9]{16}$", sid);
        }

        [Test]
        public void GenerateRandomRawId_GeneratesCorrectId()
        {
            StringAssert.IsMatch("^[A-Za-z0-9]{16}$", _client.GenerateRandomRawId());
        }
    }
}
