using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts
{
    /// <summary>
    /// Provides methods to create posts in the CLI application.
    /// </summary>
    public class CreatePostView
    {
        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<User> _userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePostView"/> class.
        /// </summary>
        /// <param name="postRepository">The repository for posts.</param>
        /// <param name="userRepository">The repository for users.</param>
        public CreatePostView(IRepository<Post> postRepository, IRepository<User> userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Prompts the user to create a new post asynchronously.
        /// </summary>
        public async Task CreatePostAsync()
        {
            Console.Write("Enter post title: ");
            var title = Console.ReadLine();
            Console.Write("Enter post body: ");
            var body = Console.ReadLine();
            Console.Write("Enter user ID: ");
            var userIdInput = Console.ReadLine();

            // Validate user ID input
            if (!int.TryParse(userIdInput, out int userId))
            {
                Console.WriteLine("Invalid user ID. Please enter a valid number.");
                return;
            }

            // Check if the user exists
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                Console.WriteLine("User ID does not exist. Please enter a valid user ID.");
                return;
            }

            // Create and add the post
            var post = new Post { Title = title, Body = body, UserId = userId };
            await _postRepository.AddAsync(post);
            Console.WriteLine("Post created successfully.");
        }
    }
}