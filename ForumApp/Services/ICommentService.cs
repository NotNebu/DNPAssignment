using ApiContracts.DTOs;

namespace ForumApp.Services
{
    public interface ICommentService
    {
        Task<CommentDto> AddCommentAsync(CreateCommentDto request);
        Task<List<CommentDto>> GetCommentsByPostIdAsync(int postId);
    }
}