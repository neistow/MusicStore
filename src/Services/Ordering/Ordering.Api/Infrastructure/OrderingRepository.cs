using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ordering.Api.Domain;
using Ordering.Api.Domain.Abstract;

namespace Ordering.Api.Infrastructure
{
    public class OrderingRepository : IOrderingRepository
    {
        private readonly OrderingContext _orderingContext;

        public IUnitOfWork UnitOfWork => _orderingContext;

        public OrderingRepository(OrderingContext orderingContext)
        {
            _orderingContext = orderingContext;
        }

        public Task<List<Checkout>> GetCustomerCheckouts(string customerId)
        {
            return _orderingContext.Checkouts.Where(c => c.CustomerId == customerId).ToListAsync();
        }

        public Task CreateCheckout(Checkout checkout)
        {
            return _orderingContext.Checkouts.AddAsync(checkout).AsTask();
        }
    }
}