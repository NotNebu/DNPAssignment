using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers
{
    public class ListUsersView
    {
        private readonly IRepository<User> _userRepository;

        public ListUsersView(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

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