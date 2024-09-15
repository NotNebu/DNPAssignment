using Entities;
using Microsoft.EntityFrameworkCore;

public class ForumDbc : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<PostLike> PostLikes { get; set; }

    public ForumDbc(DbContextOptions<ForumDbc> options) : base(options)
    {
    }

    // Dummy Data
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        var users = new List<User>
        {
            new User { Id = 1, UserName = "Alexander", Password = "1234" },
            new User { Id = 2, UserName = "Laura", Password = "4567" },
            new User { Id = 3, UserName = "Alex", Password = "8901" }
        };

        modelBuilder.Entity<User>().HasData(users);
        
        var posts = new List<Post>
        {
            new Post { Id = 1, Title = "First Post", Body = "This is the first post", UserId = 1 },
            new Post { Id = 2, Title = "Second Post", Body = "This is the second post", UserId = 2 },
            new Post { Id = 3, Title = "Third Post", Body = "This is the third post", UserId = 3 }
        };

        modelBuilder.Entity<Post>().HasData(posts);
        
        var comments = new List<Comment>
        {
            new Comment { Id = 1, Body = "Great post!", PostId = 1, UserId = 2 },
            new Comment { Id = 2, Body = "Interesting read.", PostId = 2, UserId = 3 },
            new Comment { Id = 3, Body = "Thanks for sharing.", PostId = 3, UserId = 1 }
        };

        modelBuilder.Entity<Comment>().HasData(comments);
    }
}