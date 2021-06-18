using System.Collections.Generic;
using System.Threading.Tasks;
using Ordering.Api.Application.Abstract;
using Ordering.Api.Domain;
using Ordering.Api.Domain.Abstract;

namespace Ordering.Api.Application
{
    public class OrderingService : IOrderingService
    {
        private readonly IOrderingRepository _orderingRepository;

        public OrderingService(IOrderingRepository orderingRepository)
        {
            _orderingRepository = orderingRepository;
        }

        public Task<List<Checkout>> GetCustomerCheckouts(string customerId)
        {
            return _orderingRepository.GetCustomerCheckouts(customerId);
        }
    }
}