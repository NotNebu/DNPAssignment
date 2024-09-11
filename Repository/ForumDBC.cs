using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ForumDbc : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostLike> PostLikes { get; set; }

        public ForumDbc(DbContextOptions<ForumDbc> options) : base(options)
        {
        }
        
       /* protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Post>()
                .HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        } */
    }
}