using EfcRepositories;
using Microsoft.EntityFrameworkCore;
using Entities;

namespace RepositoryContracts
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ForumDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(ForumDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            try
            {
                Console.WriteLine($"Attempting to add entity: {entity}");
                await _dbSet.AddAsync(entity); // Stage the entity for insertion
                await _context.SaveChangesAsync(); // Persist changes to the database
                Console.WriteLine("Entity added successfully.");
                return entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding entity: {ex.Message}");
                throw; // Re-throw exception for debugging
            }
        }


        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            if (typeof(T) == typeof(User))
            {
                var users = _dbSet as DbSet<User>;
                return await users!.AnyAsync(u => u.UserName == username);
            }
            throw new InvalidOperationException("UsernameExistsAsync is only applicable for User entities.");
        }
    }
}