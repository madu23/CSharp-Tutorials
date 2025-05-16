using Microsoft.EntityFrameworkCore;
using RtlEmployeeApi.Models.Entities;

namespace RtlEmployeeApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }


}
