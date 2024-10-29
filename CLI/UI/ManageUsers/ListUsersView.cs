using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers
{
    /// <summary>
    /// Provides methods to list users in the CLI application.
    /// </summary>
    public class ListUsersView
    {
        private readonly IRepository<User> _userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListUsersView"/> class.
        /// </summary>
        /// <param name="userRepository">The repository for users.</param>
        public ListUsersView(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Lists all users asynchronously.
        /// </summary>
        public async Task ListUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            foreach (var user in users)
            {
                Console.WriteLine($"ID: {user.Id}, Username: {user.UserName}");
            }
        }
    }
}