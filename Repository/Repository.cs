using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace Repository
{
    // Generisk repository implementering af IRepository
    public class Repository<T> : IRepository<T> where T : class
    {
        // Private felter til at holde på context og dbSet
        private readonly ForumDbc _context;
        private readonly DbSet<T> _dbSet;

        // Constructor til at initialisere context og dbSet
        public Repository(ForumDbc context)
        {
            // Sætter dbSet og context
            _dbSet = context.Set<T>();
            _context = context;
        }

        // Henter alle entities asynkront
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            // Returnerer alle entities
            return await _dbSet.ToListAsync();
        }

        // Tilføjer en ny entity asynkront
        public async Task<T> AddAsync(T entity)
        {
            // Tilføjer den nye entity til dbSet
            await _dbSet.AddAsync(entity);
            
            // Gemmer ændringerne i databasen
            await _context.SaveChangesAsync();
            
            // Returnerer den nye entity
            return entity;
        }

        // Henter en entity ud fra dens Id asynkront
        public async Task<T> GetByIdAsync(int id)
        {
            // Returnerer entity med det givne Id
            return await _dbSet.FindAsync(id) ?? throw new InvalidOperationException();
        }

        // Opdaterer en entity asynkront
        public async Task<T> UpdateAsync(T entity)
        {
            // Opdaterer den givne entity i dbSet
            _dbSet.Update(entity);
            
            // Gemmer ændringerne i databasen
            await _context.SaveChangesAsync();
            
            // Returnerer den opdaterede entity
            return entity;
        }

        // Sletter en entity ud fra dens Id asynkront
        public async Task<bool> DeleteAsync(int id)
        {
            // Finder entity med det givne Id
            var entity = await _dbSet.FindAsync(id);
            
            // Returnerer false, hvis entity ikke findes
            if (entity == null)
            {
                return false;
            }

            // Fjerner entity fra dbSet
            _dbSet.Remove(entity);
            
            // Gemmer ændringerne i databasen
            await _context.SaveChangesAsync();
            
            // Returnerer true, hvis entity blev slettet
            return true;
        }
        
        // Tjekker om et brugernavn eksisterer asynkront
        public async Task<bool> UsernameExistsAsync(string username)
        {
            // Tjekker om brugernavnet eksisterer i databasen
            if (typeof(T) == typeof(User))
            {
                // Returnerer true, hvis brugernavnet eksisterer
                return await _context.Users.AnyAsync(u => u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));
            }
            // Returnerer false, hvis brugernavnet ikke eksisterer
            return false;
        }
    }
}