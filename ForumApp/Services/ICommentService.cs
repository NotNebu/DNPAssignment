using ApiContracts.DTOs;

namespace ForumApp.Services
{
    /// <summary>
    /// Defines methods for comment-related operations.
    /// </summary>
    public interface ICommentService
    {
        /// <summary>
        /// Adds a new comment asynchronously.
        /// </summary>
        /// <param name="request">The comment creation request data.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the created comment data.</returns>
        Task<CommentDto> AddCommentAsync(CreateCommentDto request);

        /// <summary>
        /// Retrieves comments by post ID asynchronously.
        /// </summary>
        /// <param name="postId">The ID of the post to retrieve comments for.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of comment data.</returns>
        Task<List<CommentDto>> GetCommentsByPostIdAsync(int postId);
    }
}