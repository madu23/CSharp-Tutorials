using Microsoft.EntityFrameworkCore;
using zibaITportal.Models.Entities;

namespace zibaITportal.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ItPersonnel>  itPersonnel { get; set; }
    }
}
