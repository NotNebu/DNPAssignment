using ApiContracts.DTOs;

namespace ForumApp.Services
{
    public class HttpPostService : IPostService
    {
        private readonly HttpClient _httpClient;

        public HttpPostService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PostDto> AddPostAsync(CreatePostDto request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/posts", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<PostDto>();
        }

        public async Task<List<PostDto>> GetPostsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PostDto>>("api/posts");
        }

        public async Task<PostDto> GetPostByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<PostDto>($"api/posts/{id}");
        }
    }
}