namespace Entities;

public class PostLike
{
    public PostLike(int id, int postId, Post post, int userId, User user, bool isLiked)
    {
        Id = id;
        PostId = postId;
        Post = post;
        UserId = userId;
        User = user;
        IsLiked = isLiked;
    }

    public int Id { get; set; }
    public int PostId { get; set; }
    public Post Post { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; }
    
    public bool IsLiked { get; set; }
}