using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class InMemoryPostRepository : IRepository<Post>
{
    private List<Post> _posts;

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

    public Task<Post> AddAsync(Post entity)
    {
        entity.Id = _posts.Any() ? _posts.Max(p => p.Id) + 1 : 1;
        _posts.Add(entity);
        return Task.FromResult(entity);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var post = _posts.FirstOrDefault(p => p.Id == id);
        if (post == null) return Task.FromResult(false);

        _posts.Remove(post);
        return Task.FromResult(true);
    }

    public Task<IEnumerable<Post>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Post>>(_posts);
    }

    public Task<Post> GetByIdAsync(int id)
    {
        var post = _posts.FirstOrDefault(p => p.Id == id);
        if (post == null)
        {
            throw new InvalidOperationException("Post not found");
        }

        return Task.FromResult(post);
    }

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

    public Task<bool> UsernameExistsAsync(string username)
    {
        throw new NotSupportedException("UsernameExistsAsync is not supported for Comment entities.");
    }
}