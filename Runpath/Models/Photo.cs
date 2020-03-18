using System.Runtime.Serialization;

namespace Runpath.Models
{
    [DataContract(Name = "photo")]
    public class Photo
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }


        [DataMember(Name = "albumId")]
        public int AlbumId { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "thumbnailUrl")]
        public string ThumbnailUrl { get; set; }

    }
}