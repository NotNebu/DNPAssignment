using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class InMemoryPostLikeRepository : IRepository<PostLike>
{
    private List<PostLike> _postLikes;

    public InMemoryPostLikeRepository()
    {
        // Dummy Data
        _postLikes = new List<PostLike>
        {
            new PostLike { Id = 1, PostId = 1, UserId = 1 },
            new PostLike { Id = 2, PostId = 2, UserId = 2 }
        };
    }

    // Asynkront tilføjer en ny PostLike
    public Task<PostLike> AddAsync(PostLike entity)
    {
        entity.Id = _postLikes.Any() ? _postLikes.Max(p => p.Id) + 1 : 1;  // Auto-incrementing ID
        _postLikes.Add(entity);
        return Task.FromResult(entity);
    }

    // Asynkront sletter en PostLike
    public Task<bool> DeleteAsync(int id)
    {
        var postLike = _postLikes.FirstOrDefault(p => p.Id == id);
        if (postLike == null) return Task.FromResult(false);

        _postLikes.Remove(postLike);
        return Task.FromResult(true);
    }

    // Asynkront henter alle PostLikes
    public Task<IEnumerable<PostLike>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<PostLike>>(_postLikes);
    }

    // Asynkront henter en PostLike ud fra dens ID
    public Task<PostLike> GetByIdAsync(int id)
    {
        var postLike = _postLikes.FirstOrDefault(p => p.Id == id);
        if (postLike == null)
        {
            throw new InvalidOperationException("PostLike not found");
        }

        return Task.FromResult(postLike);
    }

    // Asynkront opdaterer en eksisterende PostLike
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

    // Asynkront tjekker om en bruger allerede har liket et opslag
    public Task<bool> UsernameExistsAsync(string username)
    {
        throw new NotSupportedException("UsernameExistsAsync is not supported for Comment entities.");
    }
}