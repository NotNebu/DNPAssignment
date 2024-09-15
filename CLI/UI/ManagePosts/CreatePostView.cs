using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts
{
    public class CreatePostView
    {
        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<User> _userRepository;

        public CreatePostView(IRepository<Post> postRepository, IRepository<User> userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

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