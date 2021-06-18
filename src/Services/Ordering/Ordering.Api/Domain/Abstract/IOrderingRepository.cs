using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ordering.Api.Domain.Abstract
{
    public interface IOrderingRepository
    {
        IUnitOfWork UnitOfWork { get; }
        Task<List<Checkout>> GetCustomerCheckouts(string customerId);
        Task CreateCheckout(Checkout checkout);
    }
}