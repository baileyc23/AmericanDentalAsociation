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
        public DbSet<Models.Course> Courses { get; set; }

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
            modelBuilder.Entity<Course>().HasData(
               new Course
               {
                   Id = 1,
                   Title = "ASP.NET Core 8 for Developers",
                   Instructor = "Lino Tadros",
                   Online = true,
                   Description = "A 3 day Training Class on ASP.NET 8 Development using the latest version of Visual Studio",
                   Price = 2250,
                   Price4 = 1999,
                   PricePrivate = 15000,
                   CategoryId = 1
           
               },

               new Course
               {
                   Id = 2,
                   Title = "Mastering Microsoft Fabric",
                   Instructor = "Lino Tadros",
                   Online = true,
                   Description = "A 3 day Training Class of Microsoft Fabric for Data Analytics",
                   Price = 3000,
                   Price4 = 2600,
                   PricePrivate = 25000,
                   CategoryId = 2

               },
               new Course
               {
                   Id = 3,
                   Title = "Mastering Sitefinity Development",
                   Instructor = "Lino Tadros",
                   Online = true,
                   Description = "A 2 day Training Class for Sitefinity development using MVC in ASP.NET",
                   Price = 1599,
                   Price4 = 1299,
                   PricePrivate = 12000,
                   CategoryId = 3

               });
        }
    }
}
