﻿using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ForumDbc _context;
        private readonly DbSet<T> _dbSet;

        public Repository(ForumDbc context)
        {
            _dbSet = context.Set<T>();
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id) ?? throw new InvalidOperationException();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
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
                return await _context.Users.AnyAsync(u => u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));
            }
            return false;
        }
    }
}