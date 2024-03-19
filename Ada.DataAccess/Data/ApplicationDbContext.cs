using Ada.Models;
using Microsoft.EntityFrameworkCore;

namespace Ada.DataAccess.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Models.Category> Categories { get; set; }

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Azure", DisplayOrder = 1 },
                new Category { Id = 2, Name = "AWS", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Sitefinity", DisplayOrder = 3 },
                new Category { Id = 4, Name = "Generative AI", DisplayOrder = 4 },
                new Category { Id = 5, Name = "Terraform", DisplayOrder = 5 },
                new Category { Id = 6, Name = "Databricks", DisplayOrder = 6 }
                );
        }
    }
}
