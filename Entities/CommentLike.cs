namespace Entities;

// Entity-klasse til at repræsentere et like på en kommentar
public class CommentLike
{

    public CommentLike()
    {
        // Constructor til at initialisere Entity Framework
    }
    
    // Constructor til at initialisere alle properties
    public CommentLike(int id, int commentId, Comment comment, int userId, User user, bool isLiked)
    {
        Id = id;
        CommentId = commentId;
        Comment = comment;
        UserId = userId;
        User = user;
        IsLiked = isLiked;
    }

    // Properties til at tilgå og ændre på CommentLike-entities
    public int Id { get; set; }
    public int CommentId { get; set; }
    public Comment Comment { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public bool IsLiked { get; set; }
    
}