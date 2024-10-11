using Entities;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace RepositoryContracts;

// Repository til at tilgå PostLike-entities i databasen
public class PostLikeRepository : IRepository<PostLike>
{
    private readonly ForumDbc _context;
    
    // Constructor til at initialisere context
    public PostLikeRepository(ForumDbc context)
    {
        _context = context;
    }

    // Tilføjer en ny PostLike til databasen asynkront
    public async Task<PostLike> AddAsync(PostLike entity)
    {
        // Tilføjer den nye PostLike til context
        _context.PostLikes.Add(entity);
        
        // Gemmer ændringerne i databasen
        await _context.SaveChangesAsync();
        
        // Returnerer den nye PostLike
        return entity;
    }

    // Sletter en PostLike fra databasen asynkront
    public async Task<bool> DeleteAsync(int id)
    {
        // Finder PostLike med det givne Id
        var entity = await _context.PostLikes.FindAsync(id);
        
        // Returnerer false, hvis PostLike ikke findes
        if (entity == null) return false;

        // Fjerner PostLike fra context
        _context.PostLikes.Remove(entity);
        
        // Gemmer ændringerne i databasen
        await _context.SaveChangesAsync();
        
        // Returnerer true, hvis PostLike er slettet
        return true;
    }

    // Returnerer alle PostLikes fra databasen asynkront
    public Task<bool> UsernameExistsAsync(string username)
    {
        // Ikke Implementeret endnu :~D
        throw new NotImplementedException();
    }

    // Returnerer alle PostLikes fra databasen asynkront
    public async Task<IEnumerable<PostLike>> GetAllAsync()
    {
        // Returnerer alle PostLikes
        return await _context.PostLikes.ToListAsync();
    }

    // Henter en PostLike ud fra dens Id asynkront
    public async Task<PostLike> GetByIdAsync(int id)
    {
        // Returnerer PostLike med det givne Id
        return await _context.PostLikes.FindAsync(id) 
               
               // Kaster en InvalidOperationException, hvis PostLike ikke findes
               ?? throw new InvalidOperationException(); 
    }

    // Opdaterer en PostLike asynkront
    public async Task<PostLike> UpdateAsync(PostLike entity)
    {
        // Opdaterer den givne PostLike i context
        _context.PostLikes.Update(entity);
        
        // Gemmer ændringerne i databasen
        await _context.SaveChangesAsync();
        
        // Returnerer den opdaterede PostLike
        return entity;
    }
}
