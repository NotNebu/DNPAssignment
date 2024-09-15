using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers
{
    public class CreateUserView
    {
        private readonly IRepository<User> _userRepository;

        public CreateUserView(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task CreateUserAsync()
        {
            Console.Write("Enter username: ");
            var username = Console.ReadLine();

            // Check if username is already taken
            var existingUsers = await _userRepository.GetAllAsync();
            if (existingUsers.Any(u => u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Username is already taken. Please choose another one.");
                return;
            }

            Console.Write("Enter password: ");
            var password = Console.ReadLine();

            // Create and add the new user
            var user = new User { UserName = username, Password = password };
            await _userRepository.AddAsync(user);
            Console.WriteLine("User created successfully.");
        }
    }
}