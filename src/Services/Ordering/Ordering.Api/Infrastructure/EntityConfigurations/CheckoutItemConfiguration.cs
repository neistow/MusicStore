using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Api.Domain;

namespace Ordering.Api.Infrastructure.EntityConfigurations
{
    public class CheckoutItemConfiguration : IEntityTypeConfiguration<CheckoutItem>
    {
        public void Configure(EntityTypeBuilder<CheckoutItem> builder)
        {
            builder.HasKey(c => new {c.CheckoutId, c.ItemId});
            builder.Property(c => c.ItemId).IsRequired();
            builder.Property(c => c.Quantity).IsRequired();
            builder.Property(c => c.PricePerUnit).IsRequired();

            builder.HasOne(c => c.Checkout)
                .WithMany(c => c.Items)
                .HasForeignKey(c => c.CheckoutId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}