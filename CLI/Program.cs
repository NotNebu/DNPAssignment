using CLI.UI;
using Entities;
using Microsoft.EntityFrameworkCore;
using Repository;
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

            IRepository<User> userRepository = new Repository<User>(context);
            IRepository<Post> postRepository = new Repository<Post>(context);
            IRepository<Comment> commentRepository = new Repository<Comment>(context);

            var cliApp = new CliApp(userRepository, postRepository, commentRepository);

            await cliApp.RunAsync();
        }
    }
}