using Entities;
using RepositoryContracts;
using FileRepository;
using WebAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped(typeof(IRepository<Post>), provider => new FileRepository<Post>("posts"));
builder.Services.AddScoped(typeof(IRepository<Comment>), provider => new FileRepository<Comment>("comments"));
builder.Services.AddScoped(typeof(IRepository<User>), provider => new FileRepository<User>("users"));
builder.Services.AddScoped(typeof(IRepository<PostLike>), provider => new FileRepository<PostLike>("postLikes"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();