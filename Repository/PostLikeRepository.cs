using Entities;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace RepositoryContracts;

public class PostLikeRepository : IRepository<PostLike>
{
    private readonly ForumDbc _context;
    
    public PostLikeRepository(ForumDbc context)
    {
        _context = context;
    }

    public async Task<PostLike> AddAsync(PostLike entity)
    {
        _context.PostLikes.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.PostLikes.FindAsync(id);
        if (entity == null) return false;

        _context.PostLikes.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public Task<bool> UsernameExistsAsync(string username)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<PostLike>> GetAllAsync()
    {
        return await _context.PostLikes.ToListAsync();
    }

    public async Task<PostLike> GetByIdAsync(int id)
    {
        return await _context.PostLikes.FindAsync(id) ?? throw new InvalidOperationException(); 
    }

    public async Task<PostLike> UpdateAsync(PostLike entity)
    {
        _context.PostLikes.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}
