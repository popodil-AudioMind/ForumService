using Microsoft.EntityFrameworkCore;
using ForumService.Interfaces;

namespace ForumService.Context
{
    public class ForumDatabaseContext : DbContext
    {
        public ForumDatabaseContext(DbContextOptions<ForumDatabaseContext> options) : base(options)
        { 
        
        }
        public DbSet<IForum> Forums { get; set; }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<IUser>()
                .Property(e => e.id)
                .HasConversion(
                    v => v.ToString(),
                    v => Guid.Parse(v));
        }*/
    }

}
