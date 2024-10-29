using CLI.UI;
using Entities;
using RepositoryContracts;

namespace CLI
{
    /// <summary>
    /// The main entry point for the CLI application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main method to start the program.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        public static async Task Main(string[] args)
        {
            // Creates a new file repository for users
            IRepository<User> userRepository = new FileRepository<User>("users");
            // Creates a new file repository for posts
            IRepository<Post> postRepository = new FileRepository<Post>("posts");
            // Creates a new file repository for comments
            IRepository<Comment> commentRepository = new FileRepository<Comment>("comments");

            // Creates a new CLI application
            var cliApp = new CliApp(userRepository, postRepository, commentRepository);

            // Runs the application
            await cliApp.RunAsync();
        }
    }
}