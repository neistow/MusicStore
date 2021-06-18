using Basket.Api.Domain;
using Basket.Api.Domain.Abstract;
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
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BasketContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}