using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

/// <summary>
/// In-memory implementation of the post repository.
/// </summary>
public class InMemoryPostRepository : IRepository<Post>
{
    private List<Post> _posts;

    /// <summary>
    /// Initializes a new instance of the <see cref="InMemoryPostRepository"/> class with dummy data.
    /// </summary>
    public InMemoryPostRepository()
    {
        // Dummy Data
        _posts = new List<Post>
        {
            new Post { Id = 1, Title = "First Post", Body = "This is the first post", UserId = 1 },
            new Post { Id = 2, Title = "Second Post", Body = "This is the second post", UserId = 2 },
            new Post { Id = 3, Title = "Third Post", Body = "This is the third post", UserId = 3 }
        };
    }

    /// <summary>
    /// Asynchronously adds a new post.
    /// </summary>
    /// <param name="entity">The post to add.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the added post.</returns>
    public Task<Post> AddAsync(Post entity)
    {
        entity.Id = _posts.Any() ? _posts.Max(p => p.Id) + 1 : 1;
        _posts.Add(entity);
        return Task.FromResult(entity);
    }

    /// <summary>
    /// Asynchronously deletes a post by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the post to delete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result indicates whether the deletion was successful.</returns>
    public Task<bool> DeleteAsync(int id)
    {
        var post = _posts.FirstOrDefault(p => p.Id == id);
        if (post == null) return Task.FromResult(false);

        _posts.Remove(post);
        return Task.FromResult(true);
    }

    /// <summary>
    /// Asynchronously gets all posts.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable of posts.</returns>
    public Task<IEnumerable<Post>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Post>>(_posts);
    }

    /// <summary>
    /// Asynchronously gets a post by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the post.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the post.</returns>
    public Task<Post> GetByIdAsync(int id)
    {
        var post = _posts.FirstOrDefault(p => p.Id == id);
        if (post == null)
        {
            throw new InvalidOperationException("Post not found");
        }

        return Task.FromResult(post);
    }

    /// <summary>
    /// Asynchronously updates an existing post.
    /// </summary>
    /// <param name="entity">The post to update.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated post.</returns>
    public Task<Post> UpdateAsync(Post entity)
    {
        var post = _posts.FirstOrDefault(p => p.Id == entity.Id);
        if (post == null)
        {
            throw new InvalidOperationException("Post not found");
        }

        post.Title = entity.Title;
        post.Body = entity.Body;
        post.UserId = entity.UserId;

        return Task.FromResult(post);
    }

    /// <summary>
    /// Throws a NotSupportedException as UsernameExistsAsync is not supported for Post entities.
    /// </summary>
    /// <param name="username">The username to check.</param>
    /// <returns>A task that represents the asynchronous operation. The task result indicates whether the username exists.</returns>
    public Task<bool> UsernameExistsAsync(string username)
    {
        throw new NotSupportedException("UsernameExistsAsync is not supported for Comment entities.");
    }
}