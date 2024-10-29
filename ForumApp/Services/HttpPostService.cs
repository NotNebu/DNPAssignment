using ApiContracts.DTOs;

namespace ForumApp.Services
{
    /// <summary>
    /// Provides HTTP-based implementations for post-related operations.
    /// </summary>
    public class HttpPostService : IPostService
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpPostService"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client used to send requests.</param>
        public HttpPostService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Adds a new post asynchronously.
        /// </summary>
        /// <param name="request">The post creation request data.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the created post data.</returns>
        public async Task<PostDto> AddPostAsync(CreatePostDto request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/posts", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<PostDto>();
        }

        /// <summary>
        /// Retrieves all posts asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of post data.</returns>
        public async Task<List<PostDto>> GetPostsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PostDto>>("api/posts");
        }

        /// <summary>
        /// Retrieves a post by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the post to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the post data.</returns>
        public async Task<PostDto> GetPostByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<PostDto>($"api/posts/{id}");
        }
    }
}