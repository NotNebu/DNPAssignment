using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EfcRepositories
{
    public class ForumDbContextFactory : IDesignTimeDbContextFactory<ForumDbContext>
    {
        public ForumDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ForumDbContext>();
            
            optionsBuilder.UseSqlite("Data Source=app.db");

            return new ForumDbContext(optionsBuilder.Options);
        }
    }
}
