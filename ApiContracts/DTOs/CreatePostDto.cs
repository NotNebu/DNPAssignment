namespace ApiContracts.DTOs
{
    /// <summary>
    /// Data Transfer Object to handle the creation of posts.
    /// </summary>
    public class CreatePostDto
    {
        /// <summary>
        /// Gets or sets the title of the post.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the body of the post.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the username of the user who created the post.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the Id of the user who created the post.
        /// </summary>
        public int UserId { get; set; }
    }
}