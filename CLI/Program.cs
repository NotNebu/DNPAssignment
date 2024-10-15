using CLI.UI;
using Entities;
using RepositoryContracts;


namespace CLI
{
    public class Program
    {
        // Main metode til at starte programmet
        public static async Task Main(string[] args)
        {
            // Laver et nyt fil repository for brugere
            IRepository<User> userRepository = new FileRepository<User>("users");
            // Laver et nyt fil repository for posts
            IRepository<Post> postRepository = new FileRepository<Post>("posts");
            // Laver et nyt fil repository for kommentarer
            IRepository<Comment> commentRepository = new FileRepository<Comment>("comments");

            // Laver et nyt CLI program
            var cliApp = new CliApp(userRepository, postRepository, commentRepository);

            // Kører programmet
            await cliApp.RunAsync();
        }
    }
}