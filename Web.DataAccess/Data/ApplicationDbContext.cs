using Microsoft.EntityFrameworkCore;
using BookWeb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace BookWeb.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "a", DisplayOrder = 10 },
                new Category { CategoryId = 2, Name = "b", DisplayOrder = 20 },
                new Category { CategoryId = 3, Name = "c", DisplayOrder = 30 }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { 
                    ProductId = 1,
                    Title = "abc",
                    Description = "Dec1", 
                    Author = "Botza", 
                    ISBN = "I1", 
                    Price = 100, 
                    PriceList = 95,
                    Price50 = 90,
                    Price100 = 85,
                    CId = 1,
                    ImageURL = ""
                },
                new Product
                {
                    ProductId = 2,
                    Title = "xyz",
                    Description = "Dec2",
                    Author = "John Doe",
                    ISBN = "I2",
                    Price = 150,
                    PriceList = 145,
                    Price50 = 140,
                    Price100 = 135,
                    CId = 1,
                    ImageURL = ""
                },
                new Product
                {
                    ProductId = 3,
                    Title = "Random Product 3",
                    Description = "Description for product 3",
                    Author = "Jane Smith",
                    ISBN = "I3",
                    Price = 120,
                    PriceList = 115,
                    Price50 = 110,
                    Price100 = 105,
                    CId = 2,
                    ImageURL = ""
                },
                new Product
                {
                    ProductId = 4,
                    Title = "Random Product 4",
                    Description = "Description for product 4",
                    Author = "Emily Clark",
                    ISBN = "I4",
                    Price = 200,
                    PriceList = 190,
                    Price50 = 180,
                    Price100 = 170,
                    CId = 2,
                    ImageURL = ""
                },
                new Product
                {
                    ProductId = 5,
                    Title = "Random Product 5",
                    Description = "Description for product 5",
                    Author = "Michael Brown",
                    ISBN = "I5",
                    Price = 175,
                    PriceList = 165,
                    Price50 = 155,
                    Price100 = 145,
                    CId = 3,
                    ImageURL = ""
                }
            );
        }
    }
}
