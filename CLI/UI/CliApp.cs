using CLI.UI.ManagePosts;
using CLI.UI.ManageUsers;
using Entities;
using RepositoryContracts;

namespace CLI.UI
{
    /// <summary>
    /// Represents the CLI application.
    /// </summary>
    public class CliApp
    {
        private readonly ManageUsersView _manageUsersView;
        private readonly ManagePostsView _managePostsView;

        /// <summary>
        /// Initializes a new instance of the <see cref="CliApp"/> class.
        /// </summary>
        /// <param name="userRepository">The repository for users.</param>
        /// <param name="postRepository">The repository for posts.</param>
        /// <param name="commentRepository">The repository for comments.</param>
        public CliApp(IRepository<User> userRepository, IRepository<Post> postRepository, IRepository<Comment> commentRepository)
        {
            _manageUsersView = new ManageUsersView(userRepository);
            _managePostsView = new ManagePostsView(postRepository, commentRepository, userRepository);
        }

        /// <summary>
        /// Runs the CLI application asynchronously.
        /// </summary>
        public async Task RunAsync()
        {
            while (true)
            {
                Console.WriteLine("1: Create User");
                Console.WriteLine("2: Create Post");
                Console.WriteLine("3: Add Comment");
                Console.WriteLine("4: View All Users");
                Console.WriteLine("5: View All Posts");
                Console.WriteLine("6: View All Comments");
                Console.WriteLine("7: Exit");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        await _manageUsersView.CreateUserAsync();
                        break;
                    case "2":
                        await _managePostsView.CreatePostAsync();
                        break;
                    case "3":
                        await _managePostsView.AddCommentAsync();
                        break;
                    case "4":
                        await _manageUsersView.ListUsersAsync();
                        break;
                    case "5":
                        await _managePostsView.ListPostsAsync();
                        break;
                    case "6":
                        await _managePostsView.ListCommentsAsync(); 
                        break;
                    case "7":
                        return;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            }
        }
    }
}