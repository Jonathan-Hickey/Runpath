using System.Collections.Generic;
using System.Threading.Tasks;
using Runpath.Models;

namespace Runpath.Services
{
    public interface IPhotoService
    {
        Task<IReadOnlyList<Photo>> GetPhotosByAlbumIdAsync(IEnumerable<int> albumIds);
    }
}