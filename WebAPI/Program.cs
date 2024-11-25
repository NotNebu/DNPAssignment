using EfcRepositories;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using WebAPI.Middleware;


namespace WebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Adds services to the application
        builder.Services.AddControllers();

        // Configure DbContext for EF Core with SQLite
        builder.Services.AddDbContext<ForumDbContext>(options =>
            options.UseSqlite(@"Data Source=C:\Users\Nebul\Documents\GitHub\DNPAssignment\EfcRepositories\app.db"));


        // Register generic repository for all entity types
        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        // Add endpoints to the application
        builder.Services.AddEndpointsApiExplorer();

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ForumDbContext>();
            DatabaseSeeder.Seed(context);
        }
        
        // Configures the application to use middleware
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        // Configures the application to use endpoints
        app.UseAuthorization();

        // Configures the application to use controllers
        app.MapControllers();

        // Runs the application
        app.Run();
    }
}