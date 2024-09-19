using CLI.UI;
using Entities;
using Microsoft.EntityFrameworkCore;
using FileRepository;
using RepositoryContracts;

namespace CLI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<ForumDbc>()
                .UseInMemoryDatabase(databaseName: "ForumDb")
                .Options;

            using var context = new ForumDbc(options);
            
            context.Database.EnsureCreated(); 
            
            IFileRepository<User> userRepository = new FileRepository<User>("users");
            IFileRepository<Post> postRepository = new FileRepository<Post>("posts");
            IFileRepository<Comment> commentRepository = new FileRepository<Comment>("comments");

            var cliApp = new CliApp(userRepository, postRepository, commentRepository);

            await cliApp.RunAsync();
        }
    }
}