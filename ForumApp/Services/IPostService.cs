using ApiContracts.DTOs;

namespace ForumApp.Services;

/// <summary>
/// Defines methods for post-related operations.
/// </summary>
public interface IPostService
{
    /// <summary>
    /// Adds a new post asynchronously.
    /// </summary>
    /// <param name="request">The post creation request data.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created post data.</returns>
    Task<PostDto> AddPostAsync(CreatePostDto request);

    /// <summary>
    /// Retrieves all posts asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of post data.</returns>
    Task<List<PostDto>> GetPostsAsync();

    /// <summary>
    /// Retrieves a post by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the post to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the post data.</returns>
    Task<PostDto> GetPostByIdAsync(int id);
}