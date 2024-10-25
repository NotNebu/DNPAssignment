using ApiContracts.DTOs;

namespace ForumApp.Services;

public interface IPostService
{
    Task<PostDto> AddPostAsync(CreatePostDto request);
    Task<List<PostDto>> GetPostsAsync();
    Task<PostDto> GetPostByIdAsync(int id);
}