using Microsoft.EntityFrameworkCore;
using RtlEmployeePortal.Models.Entities;

namespace RtlEmployeePortal.Data
{
    public class ApplicationDbContext : DbContext
    {
       
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<employee> Employees { get; set; }
    }
}
