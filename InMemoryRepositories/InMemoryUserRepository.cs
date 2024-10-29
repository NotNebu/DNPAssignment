using Entities;
using RepositoryContracts;

namespace InMemoryRepositories
{
    /// <summary>
    /// In-memory implementation of the user repository.
    /// </summary>
    public class InMemoryUserRepository : IRepository<User>
    {
        private List<User> _users;

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryUserRepository"/> class with dummy data.
        /// </summary>
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

        /// <summary>
        /// Asynchronously adds a new user.
        /// </summary>
        /// <param name="entity">The user to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the added user.</returns>
        public Task<User> AddAsync(User entity)
        {
            entity.Id = _users.Any() ? _users.Max(u => u.Id) + 1 : 1;
            _users.Add(entity);
            return Task.FromResult(entity);
        }

        /// <summary>
        /// Asynchronously deletes a user by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the user to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the deletion was successful.</returns>
        public Task<bool> DeleteAsync(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null) return Task.FromResult(false);

            _users.Remove(user);
            return Task.FromResult(true);
        }

        /// <summary>
        /// Asynchronously gets all users.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable of users.</returns>
        public Task<IEnumerable<User>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<User>>(_users);
        }

        /// <summary>
        /// Asynchronously gets a user by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the user.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the user.</returns>
        public Task<User> GetByIdAsync(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                throw new InvalidOperationException("User not found");
            }

            return Task.FromResult(user);
        }

        /// <summary>
        /// Asynchronously updates an existing user.
        /// </summary>
        /// <param name="entity">The user to update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated user.</returns>
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

        /// <summary>
        /// Asynchronously checks if a username exists.
        /// </summary>
        /// <param name="username">The username to check.</param>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the username exists.</returns>
        public Task<bool> UsernameExistsAsync(string username)
        {
            var exists = _users.Any(u => u.UserName == username);
            return Task.FromResult(exists);
        }
    }
}