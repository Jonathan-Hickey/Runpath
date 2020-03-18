using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using Runpath.Models;

namespace Runpath.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly HttpClient _httpClient;

        public AlbumService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
        public async Task<IReadOnlyList<Album>> GetAlbumsForUserAsync(int userId)
        {
            var albums = await GetAlbumsAsync();
            return albums.Where(album => album.UserId == userId).ToList();
        }

        private static readonly string GetAlbumsUrl = @"http://jsonplaceholder.typicode.com/albums";

        private async Task<IReadOnlyList<Album>> GetAlbumsAsync()
        {
            var response = await _httpClient.GetAsync(GetAlbumsUrl);

            response.EnsureSuccessStatusCode();
            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {
                return (await JsonSerializer.DeserializeAsync <Album[]>(responseStream)).ToList();
            } 
        }
    }
}
