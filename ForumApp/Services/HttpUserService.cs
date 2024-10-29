using ApiContracts.DTOs;

namespace ForumApp.Services;

/// <summary>
/// Provides HTTP-based implementations for user-related operations.
/// </summary>
public class HttpUserService : IUserService
{
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpUserService"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client used to send requests.</param>
    public HttpUserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    /// <summary>
    /// Adds a new user asynchronously.
    /// </summary>
    /// <param name="request">The user creation request data.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created user data.</returns>
    public async Task<UserDto> AddUserAsync(CreateUserDto request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/users", request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<UserDto>();
    }
    
    /// <summary>
    /// Updates an existing user asynchronously.
    /// </summary>
    /// <param name="id">The ID of the user to update.</param>
    /// <param name="request">The user update request data.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task UpdateUserAsync(int id, UpdateUserDto request)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/users/{id}", request);
        response.EnsureSuccessStatusCode();
    }
}