using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Runpath.Models;
using Runpath.Services;
using Runpath.Tests.Fakes;

namespace Runpath.Tests.Services
{
    [TestFixture()]
    public class AlbumService_GetAlbumsForUserAsync
    {
        [Test]
        public async Task When_HttpResponse_HasAlbums_ForRequestedUser_ReturnsListOfAlbums()
        {
            var expectedAlbumsUrl = @"http://jsonplaceholder.typicode.com/albums";
            var statusCode = HttpStatusCode.OK;
            var content = "[{\r\n    \"userId\": 1,\r\n    \"id\": 1,\r\n    \"title\": \"quidem molestiae enim\"\r\n  },\r\n  {\r\n    \"userId\": 1,\r\n    \"id\": 2,\r\n    \"title\": \"sunt qui excepturi placeat culpa\"\r\n  },\r\n  {\r\n    \"userId\": 1,\r\n    \"id\": 3,\r\n    \"title\": \"omnis laborum odio\"\r\n  },\r\n  {\r\n    \"userId\": 1,\r\n    \"id\": 4,\r\n    \"title\": \"non esse culpa molestiae omnis sed optio\"\r\n  }]";

            var expectedAlbums = new List<Album>
            {
                new Album{
                    UserId = 1,
                    Id = 1,
                    Title = "quidem molestiae enim"
                },
                new Album{
                    UserId = 1,
                    Id = 2,
                    Title = "sunt qui excepturi placeat culpa"
                },
                new Album{
                    UserId = 1,
                    Id = 3,
                    Title = "omnis laborum odio"
                },
                new Album{
                    UserId = 1,
                    Id = 4,
                    Title = "non esse culpa molestiae omnis sed optio"
                }
            };

            var albumService = CreateAlbumService(expectedAlbumsUrl, statusCode, content);

            var albums = await albumService.GetAlbumsForUserAsync(1);

            albums.Should().BeEquivalentTo(expectedAlbums);
        }

        [Test]
        public async Task When_HttpResponse_HasAlbums_But_NotForRequestedUser_ReturnsEmptyListOfAlbums()
        {
            var expectedAlbumsUrl = @"http://jsonplaceholder.typicode.com/albums";
            var statusCode = HttpStatusCode.OK;
            var content = "[{\r\n    \"userId\": 1,\r\n    \"id\": 1,\r\n    \"title\": \"quidem molestiae enim\"\r\n  },\r\n  {\r\n    \"userId\": 1,\r\n    \"id\": 2,\r\n    \"title\": \"sunt qui excepturi placeat culpa\"\r\n  },\r\n  {\r\n    \"userId\": 1,\r\n    \"id\": 3,\r\n    \"title\": \"omnis laborum odio\"\r\n  },\r\n  {\r\n    \"userId\": 1,\r\n    \"id\": 4,\r\n    \"title\": \"non esse culpa molestiae omnis sed optio\"\r\n  }]";

            var expectedAlbums = new List<Album> {};

            var albumService = CreateAlbumService(expectedAlbumsUrl, statusCode, content);

            var albums = await albumService.GetAlbumsForUserAsync(2);

            albums.Should().BeEquivalentTo(expectedAlbums);
        }

        [Test]
        public async Task When_HttpResponse_HasNoAlbums_ReturnsEmptyListOfAlbums()
        {
            var expectedAlbumsUrl = @"http://jsonplaceholder.typicode.com/albums";
            var statusCode = HttpStatusCode.OK;
            var content = "[]";

            var expectedAlbums = new List<Album> { };

            var albumService = CreateAlbumService(expectedAlbumsUrl, statusCode, content);

            var albums = await albumService.GetAlbumsForUserAsync(1);

            albums.Should().BeEquivalentTo(expectedAlbums);
        }

        [Test]
        public void When_HttpResponse_Has_UnsuccessfulStatusCode_ExceptionIsThrown()
        {
            var expectedAlbumsUrl = @"http://jsonplaceholder.typicode.com/albums";
            var statusCode = HttpStatusCode.InternalServerError;
            var content = "[]";

            var albumService = CreateAlbumService(expectedAlbumsUrl, statusCode, content);
            
            Assert.ThrowsAsync<HttpRequestException>(() => albumService.GetAlbumsForUserAsync(1));
        }



        private AlbumService CreateAlbumService(string expectedUrl, HttpStatusCode statusCode, string content)
        {
            var setting = new HttpTestingHandlerSettings
            {
                StatusCode = statusCode,
                ExpectedUrl = expectedUrl,
                Content = content
            };

            var client = new HttpClient(new HttpTestingHandler(setting));

            Mock<IHttpClientFactory> moqHttpClientFactory = new Mock<IHttpClientFactory>();
            moqHttpClientFactory.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(client);

            return  new AlbumService(moqHttpClientFactory.Object);
        }
    }
}
