namespace Entities;

/// <summary>
/// Entity class to represent a user.
/// </summary>
public class User
{
    /// <summary>
    /// Initializes a new instance of the <see cref="User"/> class.
    /// </summary>
    public User()
    {
        // Constructor to initialize Entity Framework
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="User"/> class with all properties.
    /// </summary>
    /// <param name="id">The identifier of the user.</param>
    /// <param name="userName">The username of the user.</param>
    /// <param name="password">The password of the user.</param>
    /// <param name="posts">The collection of posts created by the user.</param>
    /// <param name="comments">The collection of comments made by the user.</param>
    public User(int id, string userName, string password, ICollection<Post> posts, ICollection<Comment> comments)
    {
        Id = id;
        UserName = userName;
        Password = password;
        Posts = posts;
        Comments = comments;
    }

    /// <summary>
    /// Gets or sets the identifier of the user.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the username of the user.
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// Gets or sets the password of the user.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets the collection of posts created by the user.
    /// </summary>
    public ICollection<Post> Posts { get; set; }

    /// <summary>
    /// Gets or sets the collection of comments made by the user.
    /// </summary>
    public ICollection<Comment> Comments { get; set; }
}