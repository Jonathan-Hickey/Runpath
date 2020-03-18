using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Runpath.Tests.Services;

namespace Runpath.Tests.Fakes
{
    public class HttpTestingHandler : DelegatingHandler
    {
        public readonly string _content;
        public readonly HttpStatusCode _statusCode;
        public readonly string _expectedUrl;
        
        public HttpTestingHandler(HttpTestingHandlerSettings handlerSettings)
        {
            _content = handlerSettings.Content;
            _statusCode = handlerSettings.StatusCode;
            _expectedUrl = handlerSettings.ExpectedUrl;
        }

        //Note: this method can become more complex when we want to handle the same client sending multiple requests.
        //This is currently not the case so no need to add extra complexity
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            request.RequestUri.AbsoluteUri.Should().BeEquivalentTo(_expectedUrl);

            return new HttpResponseMessage()
            {
                Content = new StringContent(_content),
                StatusCode = _statusCode
            };
        }
    }
}