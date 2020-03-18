using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Runpath.Models;

namespace Runpath.Services
{
    public interface IPhotoService
    {
        IReadOnlyList<Photo> GetPhotosByAlbumId(IEnumerable<int> albumIds);
    }

    public class PhotoService : IPhotoService
    {
        public IReadOnlyList<Photo> GetPhotosByAlbumId(IEnumerable<int> albumIds)
        {
            return GetPhotos().Where(p => albumIds.Contains(p.AlbumId)).ToList();
        }

        private IReadOnlyList<Photo> GetPhotos()
        {
            return new List<Photo>();
        }
    }
}
