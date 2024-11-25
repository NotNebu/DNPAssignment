using Entities;

namespace EfcRepositories;

public static class DatabaseSeeder
{
    public static void Seed(ForumDbContext context)
    {
        // Seed Users
        if (!context.Users.Any())
        {
            var user = new User
            {
                UserName = "TestUser",
                Password = "Password123"
            };
            context.Users.Add(user);
            context.SaveChanges();
        }

        // Seed Posts
        if (!context.Posts.Any())
        {
            var user = context.Users.First();
            var post = new Post
            {
                Title = "Test Post",
                Body = "This is a test post.",
                UserId = user.Id
            };
            context.Posts.Add(post);
            context.SaveChanges();
        }

        // Seed Comments
        if (!context.Comments.Any())
        {
            var user = context.Users.First();
            var post = context.Posts.First();
            var comment = new Comment
            {
                Body = "This is a test comment.",
                UserId = user.Id,
                PostId = post.Id
            };
            context.Comments.Add(comment);
            context.SaveChanges();
        }
    }
}