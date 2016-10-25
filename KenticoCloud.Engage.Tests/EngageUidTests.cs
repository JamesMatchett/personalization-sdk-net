using System.Web;
using KenticoCloud.Engage.MVC;
using NSubstitute;
using Xunit;

namespace KenticoCloud.Engage.Tests
{
    public class EngageUidTests
    {
        public EngageUidTests()
        {
            _uid = "49d505aaa8316229";
            _sid = "93786ec335de0444";
        }

        private readonly string _uid;
        private readonly string _sid;

        [Fact]
        public void GetSidFromHttpContextTest()
        {
            var context = Substitute.For<HttpRequestBase>();
            var cookies = new HttpCookieCollection();
            var cookie = new HttpCookie("k_e_ses.whatever") {Value = $"whatever.{_sid}"};
            cookies.Add(cookie);
            context.Cookies.Returns(cookies);

            Assert.Equal(_sid, context.GetCurrentEngageSid());
        }

        [Fact]
        public void GetUidFromHttpContextTest()
        {
            var context = Substitute.For<HttpRequestBase>();
            var cookies = new HttpCookieCollection();
            var cookie = new HttpCookie("k_e_id.whatever") {Value = $"{_uid}.blablabla.blablablablabla.bla"};
            cookies.Add(cookie);
            context.Cookies.Returns(cookies);

            Assert.Equal(_uid, context.GetCurrentEngageUid());
        }
    }
}