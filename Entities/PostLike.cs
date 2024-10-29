namespace Entities;

/// <summary>
/// Entity class to represent a like on a post.
/// </summary>
public class PostLike
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PostLike"/> class.
    /// </summary>
    public PostLike()
    {
        // Constructor to initialize Entity Framework
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="PostLike"/> class with all properties.
    /// </summary>
    /// <param name="id">The identifier of the post like.</param>
    /// <param name="postId">The identifier of the post associated with the like.</param>
    /// <param name="post">The post associated with the like.</param>
    /// <param name="userId">The identifier of the user who liked the post.</param>
    /// <param name="user">The user who liked the post.</param>
    /// <param name="isLiked">Indicates whether the post is liked.</param>
    public PostLike(int id, int postId, Post post, int userId, User user, bool isLiked)
    {
        Id = id;
        PostId = postId;
        Post = post;
        UserId = userId;
        User = user;
        IsLiked = isLiked;
    }

    /// <summary>
    /// Gets or sets the identifier of the post like.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the post associated with the like.
    /// </summary>
    public int PostId { get; set; }

    /// <summary>
    /// Gets or sets the post associated with the like.
    /// </summary>
    public Post Post { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who liked the post.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Gets or sets the user who liked the post.
    /// </summary>
    public User User { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the post is liked.
    /// </summary>
    public bool IsLiked { get; set; }
}