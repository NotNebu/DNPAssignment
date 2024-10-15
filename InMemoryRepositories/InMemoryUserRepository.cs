using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class InMemoryUserRepository : IRepository<User>
{
    private List<User> _users;

    public InMemoryUserRepository()
    {
        // Dummy Data
        _users = new List<User>
        {
            new User { Id = 1, UserName = "Alexander", Password = "1234" },
            new User { Id = 2, UserName = "Laura", Password = "4567" },
            new User { Id = 3, UserName = "Alex", Password = "8901" }
        };
    }

    public Task<User> AddAsync(User entity)
    {
        entity.Id = _users.Any() ? _users.Max(u => u.Id) + 1 : 1;
        _users.Add(entity);
        return Task.FromResult(entity);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user == null) return Task.FromResult(false);

        _users.Remove(user);
        return Task.FromResult(true);
    }

    public Task<IEnumerable<User>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<User>>(_users);
    }

    public Task<User> GetByIdAsync(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            throw new InvalidOperationException("User not found");
        }

        return Task.FromResult(user);
    }

    public Task<User> UpdateAsync(User entity)
    {
        var user = _users.FirstOrDefault(u => u.Id == entity.Id);
        if (user == null)
        {
            throw new InvalidOperationException("User not found");
        }

        user.UserName = entity.UserName;
        user.Password = entity.Password;

        return Task.FromResult(user);
    }

    public Task<bool> UsernameExistsAsync(string username)
    {
        var exists = _users.Any(u => u.UserName == username);
        return Task.FromResult(exists);
    }
}