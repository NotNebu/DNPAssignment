using ApiContracts.DTOs;

namespace ForumApp.Services;

/// <summary>
/// Defines methods for user-related operations.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Adds a new user asynchronously.
    /// </summary>
    /// <param name="request">The user creation request data.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created user data.</returns>
    public Task<UserDto> AddUserAsync(CreateUserDto request);

    /// <summary>
    /// Updates an existing user asynchronously.
    /// </summary>
    /// <param name="id">The ID of the user to update.</param>
    /// <param name="request">The user update request data.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task UpdateUserAsync(int id, UpdateUserDto request);
}