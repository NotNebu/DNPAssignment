using System.Text.Json.Serialization;

namespace Domain
{
    public class Comment
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public int PostId { get; set; }

        [JsonIgnore]  
        public Post? Post { get; set; }

        public int UserId { get; set; }

        [JsonIgnore]  
        public User? User { get; set; }
    }
}