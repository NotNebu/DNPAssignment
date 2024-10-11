using Entities;
using RepositoryContracts;
using FileRepository;
using WebAPI.Middleware;

// Opretter en builder til at konfigurere applikationen
var builder = WebApplication.CreateBuilder(args);

// Tilføjer services til applikationen
builder.Services.AddControllers();

// Tilføjer endnu flere services :D 
builder.Services.AddScoped(typeof(IRepository<Post>), provider => new FileRepository<Post>("posts"));
builder.Services.AddScoped(typeof(IRepository<Comment>), provider => new FileRepository<Comment>("comments"));
builder.Services.AddScoped(typeof(IRepository<User>), provider => new FileRepository<User>("users"));
builder.Services.AddScoped(typeof(IRepository<PostLike>), provider => new FileRepository<PostLike>("postLikes"));

// Tilføjer endpoints til applikationen
builder.Services.AddEndpointsApiExplorer();

/*
Tilføjer swagger til applikationen ->
Kan ændres i launchsettings.json i mappen 'Properties' til ikke at åbne browseren :|
*/

/*
 
 "profiles": {
    "http": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true, <-- Ændre til false (Det samme for HTTPS)
      "launchUrl": "swagger",
      "applicationUrl": "http://localhost:5264",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
 
 */
builder.Services.AddSwaggerGen();

// Konfigurerer applikationen
var app = builder.Build();

// Konfigurerer applikationen til at bruge middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Beware of all the swag located here :O 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Konfigurerer applikationen til at bruge endpoints
app.UseHttpsRedirection();

// Ikke nødvendig endnu, da vi ikke har authorization 
app.UseAuthorization();

// Konfigurerer applikationen til at bruge controllers
app.MapControllers();

// Kører applikationen - Vroom Vroom 
app.Run();