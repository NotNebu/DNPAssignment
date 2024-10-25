using ForumApp.Components;
using ForumApp.Services;

namespace ForumApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5264/") });
        
        builder.Services.AddScoped<IUserService, HttpUserService>();
        builder.Services.AddScoped<IPostService, HttpPostService>();
        builder.Services.AddScoped<ICommentService, HttpCommentService>();

        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        var app = builder.Build();
        
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        // app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}