using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Runpath.Models;

namespace Runpath.Services
{
    public interface IAlbumService
    {
        IReadOnlyList<Album> GetAlbumsForUser(int userId);
    }
    public class AlbumService : IAlbumService
    {
        public IReadOnlyList<Album> GetAlbumsForUser(int userId)
        {
            return GetAlbums().Where(album => album.UserId == userId).ToList();
        }

        private IReadOnlyList<Album> GetAlbums()
        {
            return new List<Album>();
        }
    }
}
