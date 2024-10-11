using CLI.UI;
using Entities;
using Microsoft.EntityFrameworkCore;
using FileRepository;
using RepositoryContracts;

namespace CLI
{
    // Programklasse til at starte Client Test applikationen
    public class Program
    {
        // Main-metode til at starte Client Test applikationen
        public static async Task Main(string[] args)
        {
            // Opretter en in-memory database (Step 2)
            var options = new DbContextOptionsBuilder<ForumDbc>()
                .UseInMemoryDatabase(databaseName: "ForumDb")
                .Options;

            // Opretter en context til databasen
            using var context = new ForumDbc(options);
            
            // Sikrer, at databasen er oprettet
            context.Database.EnsureCreated(); 
            
            // Opretter en ny bruger for at teste applikationen
            IFileRepository<User> userRepository = new FileRepository<User>("users");
            // Opretter en ny post for at teste applikationen
            IFileRepository<Post> postRepository = new FileRepository<Post>("posts");
            // Opretter en ny kommentar for at teste applikationen
            IFileRepository<Comment> commentRepository = new FileRepository<Comment>("comments");

            // Opretter en ny CLI applikation
            var cliApp = new CliApp(userRepository, postRepository, commentRepository);

            // Kører CLI applikationen
            await cliApp.RunAsync();
        }
    }
}