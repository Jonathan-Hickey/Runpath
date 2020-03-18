using System.Collections.Generic;
using System.Threading.Tasks;
using Runpath.Models;

namespace Runpath.Services
{
    public interface IAlbumService
    {
        Task<IReadOnlyList<Album>> GetAlbumsForUserAsync(int userId);
    }
}