namespace Runpath.Dtos
{
    public class PhotoDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string ThumbnailUrl { get; set; }
        public int AlbumId { get; set; }
        public string AlbumTitle { get; set; }
        public int UserId { get; set; }
    }
}