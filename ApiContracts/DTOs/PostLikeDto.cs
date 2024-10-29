namespace ApiContracts.DTOs
{
    /// <summary>
    /// Data Transfer Object to handle likes on posts.
    /// </summary>
    public class PostLikeDto
    {
        /// <summary>
        /// Gets or sets the Id of the like.
        /// </summary>
        public int Id { get; set; } 

        /// <summary>
        /// Gets or sets the Id of the post that is liked.
        /// </summary>
        public int PostId { get; set; }

        /// <summary>
        /// Gets or sets the Id of the user who liked the post.
        /// </summary>
        public int UserId { get; set; }
    }
}