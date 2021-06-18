using Basket.Api.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Basket.Api.Infrastructure.EntityConfigurations
{
    public class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
    {
        public void Configure(EntityTypeBuilder<BasketItem> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.ItemId).IsRequired();
            builder.Property(b => b.ItemName).HasMaxLength(256).IsRequired();
            builder.Property(b => b.Quantity).IsRequired();
            builder.Property(b => b.PricePerUnit).IsRequired();

            builder.HasOne(b => b.Basket)
                .WithMany(b => b.Items)
                .HasForeignKey(b => b.BasketId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}