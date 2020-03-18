using System.Runtime.Serialization;

namespace Runpath.Models
{
    [DataContract(Name = "album")]
    public class Album
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "Title")]
        public string Title { get; set; }
        
        [DataMember(Name = "userId")]
        public int UserId { get; set; }
    }
}