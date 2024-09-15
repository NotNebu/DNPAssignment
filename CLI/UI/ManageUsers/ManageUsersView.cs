using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers
{
    public class ManageUsersView
    {
        private readonly IRepository<User> _userRepository;

        public ManageUsersView(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task CreateUserAsync()
        {
            Console.Write("Enter username: ");
            var username = Console.ReadLine();
            Console.Write("Enter password: ");
            var password = Console.ReadLine();

            var user = new User { UserName = username, Password = password };
            await _userRepository.AddAsync(user);
            Console.WriteLine("User created successfully.");
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
