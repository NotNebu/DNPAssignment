using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts
{
    /// <summary>
    /// Provides methods to add comments in the CLI application.
    /// </summary>
    public class AddCommentView
    {
        private readonly IRepository<Comment> _commentRepository;
        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<User> _userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddCommentView"/> class.
        /// </summary>
        /// <param name="commentRepository">The repository for comments.</param>
        /// <param name="postRepository">The repository for posts.</param>
        /// <param name="userRepository">The repository for users.</param>
        public AddCommentView(IRepository<Comment> commentRepository, IRepository<Post> postRepository, IRepository<User> userRepository)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Prompts the user to add a comment to a post asynchronously.
        /// </summary>
        public async Task AddCommentAsync()
        {
            Console.Write("Enter post ID: ");
            var postIdInput = Console.ReadLine();

            // Validate post ID input
            if (!int.TryParse(postIdInput, out int postId))
            {
                Console.WriteLine("Invalid post ID. Please enter a valid number.");
                return;
            }

            // Check if the post exists
            var post = await _postRepository.GetByIdAsync(postId);
            if (post == null)
            {
                Console.WriteLine("Post ID does not exist. Please enter a valid post ID.");
                return;
            }

            Console.Write("Enter comment body: ");
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

            // Create and add the comment
            var comment = new Comment { PostId = postId, Body = body, UserId = userId };
            await _commentRepository.AddAsync(comment);
            Console.WriteLine("Comment added successfully.");
        }
    }
}