using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.EntityConfigurations
{
    public class ItemTypeConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasKey(g => g.Id);
            builder.Property(g => g.Name).HasMaxLength(256).IsRequired();

            builder.HasMany(g => g.Albums)
                .WithOne(a => a.Genre)
                .HasForeignKey(a => a.GenreId);
        }
    }
}