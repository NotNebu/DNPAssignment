using ApiContracts.DTOs;

namespace ForumApp.Services;

public class HttpUserService : IUserService
{
    private readonly HttpClient _httpClient;

    public HttpUserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<UserDto> AddUserAsync(CreateUserDto request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/users", request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<UserDto>();
    }
    
    public async Task UpdateUserAsync(int id, UpdateUserDto request)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/users/{id}", request);
        response.EnsureSuccessStatusCode();
    }
}