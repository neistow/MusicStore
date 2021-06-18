using Microsoft.EntityFrameworkCore;
using Ordering.Api.Domain;
using Ordering.Api.Domain.Abstract;

namespace Ordering.Api.Infrastructure
{
    public class OrderingContext : DbContext, IUnitOfWork
    {
        public DbSet<Checkout> Checkouts { get; set; }
        public DbSet<CheckoutItem> CheckoutItems { get; set; }

        public OrderingContext(DbContextOptions<OrderingContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderingContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}