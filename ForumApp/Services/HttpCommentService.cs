using ApiContracts.DTOs;

namespace ForumApp.Services
{
    public class HttpCommentService : ICommentService
    {
        private readonly HttpClient _httpClient;

        public HttpCommentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CommentDto> AddCommentAsync(CreateCommentDto request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/comments", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CommentDto>();
        }

        public async Task<List<CommentDto>> GetCommentsByPostIdAsync(int postId)
        {
            return await _httpClient.GetFromJsonAsync<List<CommentDto>>($"api/comments?postId={postId}");        }
    }
}