using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Api.Domain;

namespace Ordering.Api.Infrastructure.EntityConfigurations
{
    public class CheckoutConfiguration : IEntityTypeConfiguration<Checkout>
    {
        public void Configure(EntityTypeBuilder<Checkout> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Date).IsRequired();
            builder.Property(c => c.CustomerId).IsRequired();

            builder.Navigation(c => c.Items).AutoInclude();

            builder.HasMany(c => c.Items)
                .WithOne(c => c.Checkout)
                .HasForeignKey(c => c.CheckoutId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}