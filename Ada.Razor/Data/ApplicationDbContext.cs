using Ada.Razor.Models;
using Microsoft.EntityFrameworkCore;

namespace Ada.Razor.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

    }
}
