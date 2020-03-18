using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Runpath.Services;

namespace Runpath.Controllers
{
    [ApiController]
    [Route("users")]
    public class PhotosController : ControllerBase
    {
  
        private readonly IPhotoMapper _photoMapper;
        private readonly IAlbumService _albumService;
        private readonly IPhotoService _photoService;

        public PhotosController(
            IAlbumService albumService,
            IPhotoService photoService,
            IPhotoMapper photoMapper)
        {
            _photoService = photoService;
            _albumService = albumService;
            _photoMapper = photoMapper;
        }

        [HttpGet]
        [Route("{userId}/photos")]
        public async Task<IActionResult> GetPhotosForUserAsync(int userId)
        
        {
            var albums = await _albumService.GetAlbumsForUserAsync(userId);

            if (!albums.Any())
            {
                return NotFound();
            }

            var albumIds = albums.Select(a => a.Id);
            var photos = await _photoService.GetPhotosByAlbumIdAsync(albumIds);

            var albumLookUp = albums.ToDictionary(a => a.Id);
            var photoDtos = photos.Select(p => _photoMapper.Map(p, albumLookUp)).ToList();

            return Ok(photoDtos);
        }
    }
}
