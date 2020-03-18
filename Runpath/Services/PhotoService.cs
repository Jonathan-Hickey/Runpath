using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Runpath.Models;

namespace Runpath.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly HttpClient _httpClient;

        public PhotoService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
        
        public async Task<IReadOnlyList<Photo>> GetPhotosByAlbumIdAsync(IEnumerable<int> albumIds)
        {
            return (await GetPhotosAsync()).Where(p => albumIds.Contains(p.AlbumId)).ToList();
        }

        private static readonly string GetPhotosUrl = "http://jsonplaceholder.typicode.com/photos";
  
        private async Task<IReadOnlyList<Photo>> GetPhotosAsync()
        {
            var response = await _httpClient.GetAsync(GetPhotosUrl);

            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {
                return await JsonSerializer.DeserializeAsync
                    <List<Photo>>(responseStream);
            }
        }
    }
}
