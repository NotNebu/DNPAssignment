using CLI.UI;
using Entities;
using Microsoft.EntityFrameworkCore;
using Repository;
using RepositoryContracts;

namespace CLI;

public class Program
{
    public static async Task Main(string[] args)
    {
        var options = new DbContextOptionsBuilder<ForumDbc>()
            .UseInMemoryDatabase(databaseName: "ForumDb")
            .Options;

        IRepository<User> userRepository = new Repository<User>(new ForumDbc(options));
        IRepository<Post> postRepository = new Repository<Post>(new ForumDbc(options));
        IRepository<Comment> commentRepository = new Repository<Comment>(new ForumDbc(options));

        var cliApp = new CliApp(userRepository, postRepository, commentRepository);

        await cliApp.RunAsync();
    }
}
