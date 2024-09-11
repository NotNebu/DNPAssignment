namespace Entities;

public class CommentLike
{

    public CommentLike()
    {
        // Constructor for EF Core
    }
    public CommentLike(int id, int commentId, Comment comment, int userId, User user, bool isLiked)
    {
        Id = id;
        CommentId = commentId;
        Comment = comment;
        UserId = userId;
        User = user;
        IsLiked = isLiked;
    }

    public int Id { get; set; }
    public int CommentId { get; set; }
    public Comment Comment { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; }
    
    public bool IsLiked { get; set; }
    
}