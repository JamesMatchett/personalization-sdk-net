using System.Collections.Generic;
using KenticoCloud.Personalization.MVC;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using NSubstitute;
using NUnit.Framework;

namespace KenticoCloud.Personalization.Tests
{
    public class HttpRequestBaseExtensionsTests
    {
        public HttpRequestBaseExtensionsTests()
        {
            _uid = "49d505aaa8316229";
            _sid = "93786ec335de0444";
        }

        private readonly string _uid;
        private readonly string _sid;

        [Test]
        public void GetSidFromHttpContextTest()
        {
            var context = Substitute.For<HttpRequest>();
            var cookies = new RequestCookieCollection(new Dictionary<string, string>() { { "k_e_ses.whatever", $"whatever.{_sid}" } });
            context.Cookies.Returns(cookies);

            Assert.AreEqual(_sid, context.GetCurrentPersonalizationSid());
        }

        [Test]
        public void GetUidFromHttpContextTest()
        {
            var context = Substitute.For<HttpRequest>();
            var cookies = new RequestCookieCollection(new Dictionary<string, string>() { { "k_e_id.whatever", $"{_uid}.blablabla.blablablablabla.bla" } });
            context.Cookies.Returns(cookies);

            Assert.AreEqual(_uid, context.GetCurrentPersonalizationUid());
        }
    }
}