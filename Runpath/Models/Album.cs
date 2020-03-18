using System.Text.Json.Serialization;

namespace Runpath.Models
{
    public class Album
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }
        
        [JsonPropertyName("userId")]
        public int UserId { get; set; }
    }
}