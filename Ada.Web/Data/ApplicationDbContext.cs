using Microsoft.EntityFrameworkCore;

namespace Ada.Web.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Models.Category> Categories { get; set; }
    }
}
