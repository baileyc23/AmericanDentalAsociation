using Ada.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Ada.API.Data
{
    public class CategoryContext : DbContext
    {
        public CategoryContext(DbContextOptions<CategoryContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
    }
}
