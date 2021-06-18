using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Basket.Api.Infrastructure.EntityConfigurations
{
    public class BasketConfiguration : IEntityTypeConfiguration<Domain.Basket>
    {
        public void Configure(EntityTypeBuilder<Domain.Basket> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.CustomerId).HasMaxLength(36).IsRequired();
            builder.Navigation(b => b.Items).AutoInclude();

            builder.HasMany(b => b.Items)
                .WithOne(b => b.Basket)
                .HasForeignKey(i => i.BasketId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}