namespace ApiContracts.DTOs
{
    /// <summary>
    /// Data Transfer Object to handle the creation of comments.
    /// </summary>
    public class CreateCommentDto
    {
        /// <summary>
        /// Gets or sets the Id of the user who made the comment.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the Id of the post the comment is associated with.
        /// </summary>
        public int PostId { get; set; }

        /// <summary>
        /// Gets or sets the body of the comment.
        /// </summary>
        public string Body { get; set; }
    }
}