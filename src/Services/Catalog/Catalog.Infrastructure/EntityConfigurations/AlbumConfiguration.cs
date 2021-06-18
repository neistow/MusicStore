using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.EntityConfigurations
{
    public class AlbumConfiguration : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasMaxLength(256).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(1024).IsRequired();
            builder.Property(p => p.CoverUrl).HasMaxLength(512);
            builder.Property(p => p.Price).IsRequired();

            builder.HasOne(p => p.Genre)
                .WithMany(g => g.Albums)
                .HasForeignKey(p => p.GenreId);
        }
    }
}