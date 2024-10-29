using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

/// <summary>
/// In-memory implementation of the comment repository.
/// </summary>
public class InMemoryCommentRepository : IRepository<Comment>
{
    private List<Comment> _comments;

    /// <summary>
    /// Initializes a new instance of the <see cref="InMemoryCommentRepository"/> class with dummy data.
    /// </summary>
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

    /// <summary>
    /// Asynchronously adds a new comment.
    /// </summary>
    /// <param name="entity">The comment to add.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the added comment.</returns>
    public Task<Comment> AddAsync(Comment entity)
    {
        entity.Id = _comments.Any() ? _comments.Max(c => c.Id) + 1 : 1;
        _comments.Add(entity);
        return Task.FromResult(entity);
    }

    /// <summary>
    /// Asynchronously deletes a comment by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the comment to delete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result indicates whether the deletion was successful.</returns>
    public Task<bool> DeleteAsync(int id)
    {
        var comment = _comments.FirstOrDefault(c => c.Id == id);
        if (comment == null) return Task.FromResult(false);

        _comments.Remove(comment);
        return Task.FromResult(true);
    }

    /// <summary>
    /// Asynchronously gets all comments.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable of comments.</returns>
    public Task<IEnumerable<Comment>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Comment>>(_comments);
    }

    /// <summary>
    /// Asynchronously gets a comment by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the comment.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the comment.</returns>
    public Task<Comment> GetByIdAsync(int id)
    {
        var comment = _comments.FirstOrDefault(c => c.Id == id);
        if (comment == null)
        {
            throw new InvalidOperationException("Comment not found");
        }

        return Task.FromResult(comment);
    }

    /// <summary>
    /// Asynchronously updates an existing comment.
    /// </summary>
    /// <param name="entity">The comment to update.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated comment.</returns>
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

    /// <summary>
    /// Throws a NotSupportedException as UsernameExistsAsync is not supported for Comment entities.
    /// </summary>
    /// <param name="username">The username to check.</param>
    /// <returns>A task that represents the asynchronous operation. The task result indicates whether the username exists.</returns>
    public async Task<bool> UsernameExistsAsync(string username)
    {
        throw new NotSupportedException("UsernameExistsAsync is not supported for Comment entities.");
    }
}