using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentSystem.Infrastructure.Models;

namespace PaymentSystem.Infrastructure.Data.EntitiesMaps
{
    public class PaymentMap : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd().HasMaxLength(36);
            builder.Property(p => p.Amount).HasColumnType("numeric(18,2)");
            builder.Property(p => p.Phone).IsRequired().HasMaxLength(20);
            builder.Property(p => p.ExternalNumber).IsRequired().HasMaxLength(64);
            builder.HasIndex(p => new {p.ExternalNumber, p.Phone}).IsUnique();
        }
    }
}