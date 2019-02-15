using System;
using System.Web;
using Moq;
using System.Web.Mvc;
using System.Web.Routing;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace Mvc.Tests
{
    // Moq helper class.
    // Source: http://www.hanselman.com/blog/ASPNETMVCSessionAtMix08TDDAndMvcMockHelpers.aspx

    public static class MoqHelpers
    {
        // creates a fake HttpContext

        public static HttpContextBase FakeHttpContext()
        {
            var context = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();
            var response = new Mock<HttpResponseBase>();
            var session = new Mock<HttpSessionStateBase>();
            var server = new Mock<HttpServerUtilityBase>();

            context.Setup(c => c.Request).Returns(request.Object);
            context.Setup(c => c.Response).Returns(response.Object);
            context.Setup(c => c.Session).Returns(session.Object);
            context.Setup(c => c.Server).Returns(server.Object);

            return context.Object;
        }

        
        // creates a fake HttpContext for a given a request Url

        public static HttpContextBase FakeHttpContext(string url)
        {
            var context = FakeHttpContext();
            context.Request.SetupRequestUrl(url);
            return context;
        }

        // creates a fake controller context. For a given controller.

        public static void SetFakeControllerContext(this Controller controller)
        {
            var httpContext = FakeHttpContext();
            var context = new ControllerContext(new RequestContext(httpContext, new RouteData()), controller);
            controller.ControllerContext = context;
        }

        // setup requested URL.

        public static void SetupRequestUrl(this HttpRequestBase request, string url)
        {
            if (url == null)
                throw new ArgumentNullException("url");

            if (!url.StartsWith("~/"))
                throw new ArgumentException("Expected a virtual url starting with '~/'");

            var mock = Mock.Get(request);

            mock.Setup(req => req.QueryString).Returns(GetQueryStringParameters(url));
            mock.Setup(req => req.AppRelativeCurrentExecutionFilePath).Returns(GetUrlFileName(url));
            mock.Setup(req => req.PathInfo).Returns(string.Empty);
        }

        // setup POST or GET method.

        public static void SetHttpMethodResult(this HttpRequestBase request, string httpMethod)
        {
            Mock.Get(request).Setup(req => req.HttpMethod).Returns(httpMethod);
        }

        private static string GetUrlFileName(string url)
        {
            if (url.Contains("?"))
                return url.Substring(0, url.IndexOf("?"));
            else
                return url;
        }

        private static NameValueCollection GetQueryStringParameters(string url)
        {
            if (url.Contains("?"))
            {
                var parameters = new NameValueCollection();

                string[] parts = url.Split("?".ToCharArray());
                string[] keys = parts[1].Split("&".ToCharArray());

                foreach (string key in keys)
                {
                    string[] part = key.Split("=".ToCharArray());
                    parameters.Add(part[0], part[1]);
                }

                return parameters;
            }
            else
            {
                return null;
            }
        }
    }
}
