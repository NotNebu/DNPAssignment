namespace Entities
{
    public class Comment
    {
        public Comment(int id, string body, int postId, Post post, int userId, User user)
        {
            Id = id;
            Body = body;
            PostId = postId;
            Post = post;
            UserId = userId;
            User = user;
        }

        public int Id { get; set; }
        public string Body { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}