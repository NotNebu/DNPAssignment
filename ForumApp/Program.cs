using ForumApp.Auth;
using ForumApp.Components;
using ForumApp.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace ForumApp;

public class Program
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    /// <param name="args">The command-line arguments.</param>
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Configure HttpClient with a base address
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5264/") });
        
        // Register application services
        builder.Services.AddScoped<IUserService, HttpUserService>();
        builder.Services.AddScoped<IPostService, HttpPostService>();
        builder.Services.AddScoped<ICommentService, HttpCommentService>();
        builder.Services.AddScoped<AuthenticationStateProvider, SimpleAuthProvider>();
        
        // Add Razor components and enable interactive server components
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        var app = builder.Build();
        
        // Configure error handling and HSTS for non-development environments
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        // Uncomment to enable HTTPS redirection
        // app.UseHttpsRedirection();

        // Serve static files
        app.UseStaticFiles();
        app.UseAntiforgery();

        // Map Razor components and enable interactive server render mode
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        // Run the application
        app.Run();
    }
}