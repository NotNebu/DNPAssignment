using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class InMemoryCommentRepository : IRepository<Comment>
{
    private List<Comment> _comments;

    public InMemoryCommentRepository()
    {
        // Dummy Data
        _comments = new List<Comment>
        {
            new Comment { Id = 1, Body = "Great post!", PostId = 1, UserId = 2 },
            new Comment { Id = 2, Body = "Interesting read.", PostId = 2, UserId = 3 },
            new Comment { Id = 3, Body = "Thanks for sharing.", PostId = 3, UserId = 1 }
        };
    }

    public Task<Comment> AddAsync(Comment entity)
    {
        entity.Id = _comments.Any() ? _comments.Max(c => c.Id) + 1 : 1;
        _comments.Add(entity);
        return Task.FromResult(entity);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var comment = _comments.FirstOrDefault(c => c.Id == id);
        if (comment == null) return Task.FromResult(false);

        _comments.Remove(comment);
        return Task.FromResult(true);
    }

    public Task<IEnumerable<Comment>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Comment>>(_comments);
    }

    public Task<Comment> GetByIdAsync(int id)
    {
        var comment = _comments.FirstOrDefault(c => c.Id == id);
        if (comment == null)
        {
            throw new InvalidOperationException("Comment not found");
        }

        return Task.FromResult(comment);
    }

    public Task<Comment> UpdateAsync(Comment entity)
    {
        var comment = _comments.FirstOrDefault(c => c.Id == entity.Id);
        if (comment == null)
        {
            throw new InvalidOperationException("Comment not found");
        }

        comment.Body = entity.Body;
        comment.PostId = entity.PostId;
        comment.UserId = entity.UserId;

        return Task.FromResult(comment);
    }

    public async Task<bool> UsernameExistsAsync(string username)
    {
        throw new NotSupportedException("UsernameExistsAsync is not supported for Comment entities.");
    }
}