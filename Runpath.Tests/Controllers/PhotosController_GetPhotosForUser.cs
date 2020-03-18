using System.Collections.Generic;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Runpath.Controllers;
using Runpath.Dtos;
using Runpath.Mappers;
using Runpath.Models;
using Runpath.Services;

namespace Runpath.Tests.Controllers
{
    [TestFixture]
    public class PhotosController_GetPhotosForUser
    {
        [Test]
        public void WhenUserHasAlbums_And_AlbumsHavePhotos_ShouldReturn_Http_Ok_WithPhotoDtos()
        {
            var albums = new List<Album> {new Album {Id = 1, Title = "quidem molestiae enim", UserId = 1}};

            var photos = new List<Photo>
            {
                new Photo
                {
                    AlbumId = 1,
                    Id = 1,
                    Title = "accusamus beatae ad facilis cum similique qui sunt",
                    Url = "https=//via.placeholder.com/600/92c952",
                    ThumbnailUrl = "https=//via.placeholder.com/150/92c952"
                },
                new Photo
                {
                    AlbumId = 1,
                    Id = 2,
                    Title = "reprehenderit est deserunt velit ipsam",
                    Url = "https=//via.placeholder.com/600/771796",
                    ThumbnailUrl = "https=//via.placeholder.com/150/771796"
                },
            };

            var expectedPhotoDtos = new List<PhotoDto>
            {
                new PhotoDto
                {
                    AlbumId = 1,
                    Id = 1,
                    Title = "accusamus beatae ad facilis cum similique qui sunt",
                    Url = "https=//via.placeholder.com/600/92c952",
                    ThumbnailUrl = "https=//via.placeholder.com/150/92c952", 
                    UserId = 1,
                    AlbumTitle = "quidem molestiae enim"
                },
                new PhotoDto
                {
                    AlbumId = 1,
                    Id = 2,
                    Title = "reprehenderit est deserunt velit ipsam",
                    Url = "https=//via.placeholder.com/600/771796",
                    ThumbnailUrl = "https=//via.placeholder.com/150/771796",
                    AlbumTitle = "quidem molestiae enim",
                    UserId = 1,
                }
            };
            
            var photoController = CreatePhotosController(albums, photos);

            var result = photoController.GetPhotosForUser(1);

            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;

            okResult.Should().NotBe(null);
            okResult.StatusCode.Should().Be(200);

            var photoDtos = okResult.Value as List<PhotoDto>;
            photoDtos.Should().BeEquivalentTo(expectedPhotoDtos);
        }


        [Test]
        public void WhenUserHasAlbums_And_AlbumsHaveNoPhotos_ShouldReturn_Http_Ok_WithEmptyPhotoDtos()
        {
            var albums = new List<Album> { new Album { Id = 1, Title = "quidem molestiae enim", UserId = 1 } };

            var photos = new List<Photo> {};

            var expectedPhotoDtos = new List<PhotoDto> {};

            var photoController = CreatePhotosController(albums, photos);

            var result = photoController.GetPhotosForUser(1);

            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;

            okResult.Should().NotBe(null);
            okResult.StatusCode.Should().Be(200);

            var photoDtos = okResult.Value as List<PhotoDto>;
            photoDtos.Should().BeEquivalentTo(expectedPhotoDtos);
        }


        [Test]
        public void WhenUserHasNoAlbums_ShouldReturn_Http_NotFound_WithNoPhotoDtos()
        {
            var albums = new List<Album> { };

            var photoController = CreatePhotosController(albums, new List<Photo>());

            var result = photoController.GetPhotosForUser(1);

            result.Should().BeOfType<NotFoundResult>();
            var notFoundObjectResult = result as NotFoundResult;

            notFoundObjectResult.Should().NotBe(null);
            notFoundObjectResult.StatusCode.Should().Be(404);

        }


        public PhotosController CreatePhotosController(IReadOnlyList<Album> albums, IReadOnlyList<Photo> photos)
        {
            Mock<IAlbumService> moqAlbumService = new Mock<IAlbumService>();
            moqAlbumService.Setup(a => a.GetAlbumsForUser(It.IsAny<int>())).Returns(albums);


            Mock<IPhotoService> moqPhotoService = new Mock<IPhotoService>();
            moqPhotoService.Setup(p => p.GetPhotosByAlbumId(It.IsAny<IEnumerable<int>>())).Returns(photos);

            return new PhotosController(moqAlbumService.Object, moqPhotoService.Object, new PhotoMapper());
        }
    }
}
