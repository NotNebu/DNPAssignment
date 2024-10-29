namespace Entities;

/// <summary>
/// Entity class to represent a post.
/// </summary>
public class Post
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Post"/> class.
    /// </summary>
    public Post()
    {
        // Constructor to initialize Entity Framework
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="Post"/> class with all properties.
    /// </summary>
    /// <param name="id">The identifier of the post.</param>
    /// <param name="title">The title of the post.</param>
    /// <param name="body">The body of the post.</param>
    /// <param name="userId">The identifier of the user who created the post.</param>
    /// <param name="user">The user who created the post.</param>
    /// <param name="comments">The collection of comments associated with the post.</param>
    public Post(int id, string title, string body, int userId, User user, ICollection<Comment> comments)
    {
        Id = id;
        Title = title;
        Body = body;
        UserId = userId;
        User = user;
        Comments = comments;
    }

    /// <summary>
    /// Gets or sets the identifier of the post.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the post.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the body of the post.
    /// </summary>
    public string Body { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who created the post.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Gets or sets the user who created the post.
    /// </summary>
    public User User { get; set; }

    /// <summary>
    /// Gets or sets the collection of comments associated with the post.
    /// </summary>
    public ICollection<Comment> Comments { get; set; }
}