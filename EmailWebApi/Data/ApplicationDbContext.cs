using EmailWebApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace EmailWebApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<EmailLog> emailLogs { get; set; }
    }
    public class EmailLog
    {
        public int Id { get; set; }
        public required string to { get; set; }
        public string? subject { get; set; }
        public required string body { get; set; }
    }
}
