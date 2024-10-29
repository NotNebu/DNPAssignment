using Entities;
using RepositoryContracts;
using WebAPI.Middleware;

/// <summary>
/// Configures and runs the web application.
/// </summary>
public class Program
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    /// <param name="args">The command-line arguments.</param>
    public static void Main(string[] args)
    {
        // Creates a builder to configure the application
        var builder = WebApplication.CreateBuilder(args);

        // Adds services to the application
        builder.Services.AddControllers();

        // Adds scoped services for repositories
        builder.Services.AddScoped(typeof(IRepository<Post>), provider => new FileRepository<Post>("posts"));
        builder.Services.AddScoped(typeof(IRepository<Comment>), provider => new FileRepository<Comment>("comments"));
        builder.Services.AddScoped(typeof(IRepository<User>), provider => new FileRepository<User>("users"));
        builder.Services.AddScoped(typeof(IRepository<PostLike>), provider => new FileRepository<PostLike>("postLikes"));

        // Adds endpoints to the application
        builder.Services.AddEndpointsApiExplorer();
        

        // Configures the application
        var app = builder.Build();

        // Configures the application to use middleware
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        
        // Configures the application to use endpoints
        // app.UseHttpsRedirection();

        app.UseAuthorization();

        // Configures the application to use controllers
        app.MapControllers();

        // Runs the application
        app.Run();
    }
}