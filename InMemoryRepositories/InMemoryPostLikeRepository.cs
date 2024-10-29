using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

/// <summary>
/// In-memory implementation of the post like repository.
/// </summary>
public class InMemoryPostLikeRepository : IRepository<PostLike>
{
    private List<PostLike> _postLikes;

    /// <summary>
    /// Initializes a new instance of the <see cref="InMemoryPostLikeRepository"/> class with dummy data.
    /// </summary>
    public InMemoryPostLikeRepository()
    {
        // Dummy Data
        _postLikes = new List<PostLike>
        {
            new PostLike { Id = 1, PostId = 1, UserId = 1 },
            new PostLike { Id = 2, PostId = 2, UserId = 2 }
        };
    }

    /// <summary>
    /// Asynchronously adds a new post like.
    /// </summary>
    /// <param name="entity">The post like to add.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the added post like.</returns>
    public Task<PostLike> AddAsync(PostLike entity)
    {
        entity.Id = _postLikes.Any() ? _postLikes.Max(p => p.Id) + 1 : 1;  // Auto-incrementing ID
        _postLikes.Add(entity);
        return Task.FromResult(entity);
    }

    /// <summary>
    /// Asynchronously deletes a post like by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the post like to delete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result indicates whether the deletion was successful.</returns>
    public Task<bool> DeleteAsync(int id)
    {
        var postLike = _postLikes.FirstOrDefault(p => p.Id == id);
        if (postLike == null) return Task.FromResult(false);

        _postLikes.Remove(postLike);
        return Task.FromResult(true);
    }

    /// <summary>
    /// Asynchronously gets all post likes.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable of post likes.</returns>
    public Task<IEnumerable<PostLike>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<PostLike>>(_postLikes);
    }

    /// <summary>
    /// Asynchronously gets a post like by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the post like.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the post like.</returns>
    public Task<PostLike> GetByIdAsync(int id)
    {
        var postLike = _postLikes.FirstOrDefault(p => p.Id == id);
        if (postLike == null)
        {
            throw new InvalidOperationException("PostLike not found");
        }

        return Task.FromResult(postLike);
    }

    /// <summary>
    /// Asynchronously updates an existing post like.
    /// </summary>
    /// <param name="entity">The post like to update.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated post like.</returns>
    public Task<PostLike> UpdateAsync(PostLike entity)
    {
        var postLike = _postLikes.FirstOrDefault(p => p.Id == entity.Id);
        if (postLike == null)
        {
            throw new InvalidOperationException("PostLike not found");
        }

        postLike.PostId = entity.PostId;
        postLike.UserId = entity.UserId;
        
        return Task.FromResult(postLike);
    }

    /// <summary>
    /// Throws a NotSupportedException as UsernameExistsAsync is not supported for PostLike entities.
    /// </summary>
    /// <param name="username">The username to check.</param>
    /// <returns>A task that represents the asynchronous operation. The task result indicates whether the username exists.</returns>
    public Task<bool> UsernameExistsAsync(string username)
    {
        throw new NotSupportedException("UsernameExistsAsync is not supported for Comment entities.");
    }
}