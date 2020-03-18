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
    public class PhotoService_GetPhotosByAlbumIdAsync
    {
        [Test]
        public async Task When_HttpResponse_HasPhotos_ForRequestedAlbum_ReturnsListOfPhotos()
        {
            var expectedPhotosUrl = "http://jsonplaceholder.typicode.com/photos";
            var statusCode = HttpStatusCode.OK;
            var content = "[\r\n  {\r\n    \"albumId\": 1,\r\n    \"id\": 1,\r\n    \"title\": \"accusamus beatae ad facilis cum similique qui sunt\",\r\n    \"url\": \"https://via.placeholder.com/600/92c952\",\r\n    \"thumbnailUrl\": \"https://via.placeholder.com/150/92c952\"\r\n  },\r\n  {\r\n    \"albumId\": 1,\r\n    \"id\": 2,\r\n    \"title\": \"reprehenderit est deserunt velit ipsam\",\r\n    \"url\": \"https://via.placeholder.com/600/771796\",\r\n    \"thumbnailUrl\": \"https://via.placeholder.com/150/771796\"\r\n  },\r\n  {\r\n    \"albumId\": 1,\r\n    \"id\": 3,\r\n    \"title\": \"officia porro iure quia iusto qui ipsa ut modi\",\r\n    \"url\": \"https://via.placeholder.com/600/24f355\",\r\n    \"thumbnailUrl\": \"https://via.placeholder.com/150/24f355\"\r\n  },\r\n  {\r\n    \"albumId\": 1,\r\n    \"id\": 4,\r\n    \"title\": \"culpa odio esse rerum omnis laboriosam voluptate repudiandae\",\r\n    \"url\": \"https://via.placeholder.com/600/d32776\",\r\n    \"thumbnailUrl\": \"https://via.placeholder.com/150/d32776\"\r\n  }]";

            var expectedPhotos = new List<Photo>
            {
                new Photo
                {
                    AlbumId = 1,
                    Id = 1,
                    Title = "accusamus beatae ad facilis cum similique qui sunt",
                    Url = "https://via.placeholder.com/600/92c952",
                    ThumbnailUrl = "https://via.placeholder.com/150/92c952"
                },
                new Photo
                {
                    AlbumId = 1,
                    Id = 2,
                    Title = "reprehenderit est deserunt velit ipsam",
                    Url = "https://via.placeholder.com/600/771796",
                    ThumbnailUrl = "https://via.placeholder.com/150/771796"
                },
                new Photo
                {
                    AlbumId = 1,
                    Id = 3,
                    Title = "officia porro iure quia iusto qui ipsa ut modi",
                    Url = "https://via.placeholder.com/600/24f355",
                    ThumbnailUrl = "https://via.placeholder.com/150/24f355"
                },
                new Photo
                {
                    AlbumId = 1,
                    Id = 4,
                    Title = "culpa odio esse rerum omnis laboriosam voluptate repudiandae",
                    Url = "https://via.placeholder.com/600/d32776",
                    ThumbnailUrl = "https://via.placeholder.com/150/d32776"
                }
            };

            var photoService = CreatePhotoService(expectedPhotosUrl, statusCode, content);

            var photos = await photoService.GetPhotosByAlbumIdAsync(new List<int> { 1 });

            photos.Should().BeEquivalentTo(expectedPhotos);
        }

        [Test]
        public async Task When_HttpResponse_HasPhotos_But_NotForRequestedAlbum_ReturnsEmptyListOfPhotos()
        {
            var expectedPhotosUrl = "http://jsonplaceholder.typicode.com/photos";
            var statusCode = HttpStatusCode.OK;
            var content = "[\r\n  {\r\n    \"albumId\": 1,\r\n    \"id\": 1,\r\n    \"title\": \"accusamus beatae ad facilis cum similique qui sunt\",\r\n    \"url\": \"https://via.placeholder.com/600/92c952\",\r\n    \"thumbnailUrl\": \"https://via.placeholder.com/150/92c952\"\r\n  },\r\n  {\r\n    \"albumId\": 1,\r\n    \"id\": 2,\r\n    \"title\": \"reprehenderit est deserunt velit ipsam\",\r\n    \"url\": \"https://via.placeholder.com/600/771796\",\r\n    \"thumbnailUrl\": \"https://via.placeholder.com/150/771796\"\r\n  },\r\n  {\r\n    \"albumId\": 1,\r\n    \"id\": 3,\r\n    \"title\": \"officia porro iure quia iusto qui ipsa ut modi\",\r\n    \"url\": \"https://via.placeholder.com/600/24f355\",\r\n    \"thumbnailUrl\": \"https://via.placeholder.com/150/24f355\"\r\n  },\r\n  {\r\n    \"albumId\": 1,\r\n    \"id\": 4,\r\n    \"title\": \"culpa odio esse rerum omnis laboriosam voluptate repudiandae\",\r\n    \"url\": \"https://via.placeholder.com/600/d32776\",\r\n    \"thumbnailUrl\": \"https://via.placeholder.com/150/d32776\"\r\n  }]";

            var expectedPhotos = new List<Photo> { };

            var photoService = CreatePhotoService(expectedPhotosUrl, statusCode, content);

            var photos = await photoService.GetPhotosByAlbumIdAsync(new List<int> { 2 });

            photos.Should().BeEquivalentTo(expectedPhotos);
        }


        [Test]
        public async Task When_HttpResponse_HasNoPhotos_ReturnsEmptyListOfPhotos()
        {
            var expectedPhotosUrl = "http://jsonplaceholder.typicode.com/photos";
            var statusCode = HttpStatusCode.OK;
            var content = "[]";

            var expectedPhotos = new List<Photo> { };

            var photoService = CreatePhotoService(expectedPhotosUrl, statusCode, content);

            var photos = await photoService.GetPhotosByAlbumIdAsync(new List<int> { 1 });

            photos.Should().BeEquivalentTo(expectedPhotos);
        }

        private PhotoService CreatePhotoService(string expectedUrl, HttpStatusCode statusCode, string content)
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

            return  new PhotoService(moqHttpClientFactory.Object);
        }
    }
}
