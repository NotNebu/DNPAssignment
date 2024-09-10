using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class IPostLikeRepository : IRepository<PostLike>
{
    private readonly ForumDBC _context;
    
    public IPostLikeRepository(ForumDBC context)
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

    public async Task<IEnumerable<PostLike>> GetAllAsync()
    {
        return await _context.PostLikes.ToListAsync();
    }

    public async Task<PostLike> GetByIdAsync(int id)
    {
        return await _context.PostLikes.FindAsync(id);
    }

    public async Task<PostLike> UpdateAsync(PostLike entity)
    {
        _context.PostLikes.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}
