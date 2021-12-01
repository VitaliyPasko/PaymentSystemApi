using Microsoft.EntityFrameworkCore;
using PaymentSystem.ApplicationLayer.Data.EntitiesMaps;
using PaymentSystem.ApplicationLayer.Services.PaymentService.Models;

namespace PaymentSystem.ApplicationLayer.Data
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