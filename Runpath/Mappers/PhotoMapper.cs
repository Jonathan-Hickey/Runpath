using System;
using System.Collections.Generic;
using Runpath.Controllers;
using Runpath.Dtos;
using Runpath.Models;

namespace Runpath.Mappers
{
    public class PhotoMapper : IPhotoMapper
    {
        public PhotoDto Map(Photo photo, IReadOnlyDictionary<int, Album> albumLookUp)
        {
            if (!albumLookUp.ContainsKey(photo.AlbumId))
            {
                throw new ArgumentException($"AlbumLookUp doesn't contain Album Id: {photo.AlbumId}");
            }

            var album = albumLookUp[photo.AlbumId];

            return new PhotoDto
            {
                AlbumId = album.Id,
                AlbumTitle = album.Title,
                Id = photo.Id,
                Title = photo.Title,
                ThumbnailUrl = photo.ThumbnailUrl,
                Url = photo.Url,
                UserId = album.UserId
            };
        }
    }
}