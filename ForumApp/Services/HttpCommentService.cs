using ApiContracts.DTOs;
using Microsoft.AspNetCore.Components.Authorization;

namespace ForumApp.Services
{
    /// <summary>
    /// Provides HTTP-based implementations for comment-related operations.
    /// </summary>
    public class HttpCommentService : ICommentService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authProvider;

        public HttpCommentService(HttpClient httpClient, AuthenticationStateProvider authProvider)
        {
            _httpClient = httpClient;
            _authProvider = authProvider;
        }

        public async Task<CommentDto> AddCommentAsync(CreateCommentDto request)
        {
            // Extract the UserId from the current authenticated user
            var authState = await _authProvider.GetAuthenticationStateAsync();
            var userIdClaim = authState.User.FindFirst("Id")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                throw new Exception("User is not authenticated or UserId is invalid.");
            }

            Console.WriteLine($"Extracted UserId: {userId}");

            request.UserId = userId; // Assign the UserId to the comment

            var response = await _httpClient.PostAsJsonAsync("api/comments", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CommentDto>();
        }

        
        public async Task<List<CommentDto>> GetCommentsByPostIdAsync(int postId)
        {
            return await _httpClient.GetFromJsonAsync<List<CommentDto>>($"api/comments?postId={postId}");
        }
    }
}