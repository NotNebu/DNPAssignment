namespace Entities
{
    /// <summary>
    /// Entity class to represent a comment.
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Comment"/> class.
        /// </summary>
        public Comment()
        {
            // Constructor to initialize Entity Framework    
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Comment"/> class with all properties.
        /// </summary>
        /// <param name="id">The identifier of the comment.</param>
        /// <param name="body">The body of the comment.</param>
        /// <param name="postId">The identifier of the post associated with the comment.</param>
        /// <param name="post">The post associated with the comment.</param>
        /// <param name="userId">The identifier of the user who made the comment.</param>
        /// <param name="user">The user who made the comment.</param>
        public Comment(int id, string body, int postId, Post post, int userId, User user)
        {
            Id = id;
            Body = body;
            PostId = postId;
            Post = post;
            UserId = userId;
            User = user;
        }

        /// <summary>
        /// Gets or sets the identifier of the comment.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the body of the comment.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the post associated with the comment.
        /// </summary>
        public int PostId { get; set; }

        /// <summary>
        /// Gets or sets the post associated with the comment.
        /// </summary>
        public Post Post { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who made the comment.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the user who made the comment.
        /// </summary>
        public User User { get; set; }
    }
}