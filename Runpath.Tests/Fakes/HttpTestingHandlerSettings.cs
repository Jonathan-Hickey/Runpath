using System.Net;

namespace Runpath.Tests.Fakes
{
    public class HttpTestingHandlerSettings
    {
        public string Content { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string ExpectedUrl { get; set; }
    }
}