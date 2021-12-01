using Microsoft.EntityFrameworkCore;
using PaymentSystem.Infrastructure.Data.EntitiesMaps;
using PaymentSystem.Infrastructure.Models;

namespace PaymentSystem.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Payment> Payments { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new PaymentMap());
        }
    }
}