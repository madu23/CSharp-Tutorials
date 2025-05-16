using Microsoft.EntityFrameworkCore;
using ZibaEmployeePortal.Models.Entities;

namespace ZibaEmployeePortal.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees{ get; set; }
    }
}
