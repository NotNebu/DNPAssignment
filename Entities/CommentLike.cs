namespace Entities;

/// <summary>
/// Entity class to represent a like on a comment.
/// </summary>
public class CommentLike
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CommentLike"/> class.
    /// </summary>
    public CommentLike()
    {
        // Constructor to initialize Entity Framework
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="CommentLike"/> class with all properties.
    /// </summary>
    /// <param name="id">The identifier of the comment like.</param>
    /// <param name="commentId">The identifier of the comment associated with the like.</param>
    /// <param name="comment">The comment associated with the like.</param>
    /// <param name="userId">The identifier of the user who liked the comment.</param>
    /// <param name="user">The user who liked the comment.</param>
    /// <param name="isLiked">Indicates whether the comment is liked.</param>
    public CommentLike(int id, int commentId, Comment comment, int userId, User user, bool isLiked)
    {
        Id = id;
        CommentId = commentId;
        Comment = comment;
        UserId = userId;
        User = user;
        IsLiked = isLiked;
    }

    /// <summary>
    /// Gets or sets the identifier of the comment like.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the comment associated with the like.
    /// </summary>
    public int CommentId { get; set; }

    /// <summary>
    /// Gets or sets the comment associated with the like.
    /// </summary>
    public Comment Comment { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who liked the comment.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Gets or sets the user who liked the comment.
    /// </summary>
    public User User { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the comment is liked.
    /// </summary>
    public bool IsLiked { get; set; }
}