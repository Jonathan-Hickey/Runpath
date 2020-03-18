using System.Collections.Generic;
using Runpath.Dtos;
using Runpath.Models;

namespace Runpath.Controllers
{
    public interface IPhotoMapper
    {
        PhotoDto Map(Photo photo, IReadOnlyDictionary<int, Album> albumLookUp);
    }
}