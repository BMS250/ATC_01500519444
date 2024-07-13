using Microsoft.EntityFrameworkCore;
using WebApplication1_Razor.Models;

namespace WebApplication1_Razor.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "a", DisplayOrder = 10 },
                new Category { CategoryId = 2, Name = "b", DisplayOrder = 20 },
                new Category { CategoryId = 3, Name = "c", DisplayOrder = 30 }
            );
        }
    }
}
