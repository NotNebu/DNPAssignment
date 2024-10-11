namespace Entities;

// Entity-klasse til at repræsentere et like på et opslag
public class PostLike
{
    public PostLike()
    {
        // Constructor til at initialisere Entity Framework
    }
    
    // Constructor til at initialisere alle properties
    public PostLike(int id, int postId, Post post, int userId, User user, bool isLiked)
    {
        Id = id;
        PostId = postId;
        Post = post;
        UserId = userId;
        User = user;
        IsLiked = isLiked;
    }

    // Properties til at tilgå og ændre på PostLike-entities
    public int Id { get; set; }
    public int PostId { get; set; }
    public Post Post { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public bool IsLiked { get; set; }
}