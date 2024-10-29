using Entities;
using RepositoryContracts;

/// <summary>
/// Provides methods to manage users in the CLI application.
/// </summary>
public class ManageUsersView
{
    private readonly IRepository<User> _userRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="ManageUsersView"/> class.
    /// </summary>
    /// <param name="userRepository">The repository for users.</param>
    public ManageUsersView(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Prompts the user to create a new user asynchronously.
    /// </summary>
    public async Task CreateUserAsync()
    {
        Console.Write("Enter username: ");
        var username = Console.ReadLine();

        // Check if username is already taken
        if (await _userRepository.UsernameExistsAsync(username))
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