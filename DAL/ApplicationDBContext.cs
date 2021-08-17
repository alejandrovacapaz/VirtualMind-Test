using Microsoft.EntityFrameworkCore;
using VirtualMind.Model;

namespace VirtualMind.DAL
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Limit> Limits { get; set; }
        public ApplicationDBContext() : base()
        {
        }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Purchase>().ToTable("Purchases");
            builder.Entity<Limit>().ToTable("Limits");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=virtualMind.db");
        }
    }
}
