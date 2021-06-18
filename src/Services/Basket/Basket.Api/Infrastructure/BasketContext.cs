using Basket.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace Basket.Api.Infrastructure
{
    public class BasketContext : DbContext, IUnitOfWork
    {
        public DbSet<Domain.Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }

        public BasketContext(DbContextOptions<BasketContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Basket>()
                .HasKey(b => b.CustomerId);
            modelBuilder.Entity<Domain.Basket>()
                .Property(b => b.CustomerId).ValueGeneratedNever();
            modelBuilder.Entity<Domain.Basket>().Navigation(b => b.Items).AutoInclude();

            base.OnModelCreating(modelBuilder);
        }
    }
}