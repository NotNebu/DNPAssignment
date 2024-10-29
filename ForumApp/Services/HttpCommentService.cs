using ApiContracts.DTOs;

namespace ForumApp.Services
{
    /// <summary>
    /// Provides HTTP-based implementations for comment-related operations.
    /// </summary>
    public class HttpCommentService : ICommentService
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpCommentService"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client used to send requests.</param>
        public HttpCommentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Adds a new comment asynchronously.
        /// </summary>
        /// <param name="request">The comment creation request data.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the created comment data.</returns>
        public async Task<CommentDto> AddCommentAsync(CreateCommentDto request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/comments", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CommentDto>();
        }

        /// <summary>
        /// Retrieves comments by post ID asynchronously.
        /// </summary>
        /// <param name="postId">The ID of the post to retrieve comments for.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of comment data.</returns>
        public async Task<List<CommentDto>> GetCommentsByPostIdAsync(int postId)
        {
            return await _httpClient.GetFromJsonAsync<List<CommentDto>>($"api/comments?postId={postId}");
        }
    }
}