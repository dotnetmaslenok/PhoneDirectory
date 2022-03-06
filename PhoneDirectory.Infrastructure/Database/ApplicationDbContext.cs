using Microsoft.EntityFrameworkCore;
using PhoneDirectory.Domain.Entities;

namespace PhoneDirectory.Infrastructure.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        
        public DbSet<Division> Divisions { get; set; }

        public DbSet<ApplicationUser> Users { get; set; }

        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
    }
}