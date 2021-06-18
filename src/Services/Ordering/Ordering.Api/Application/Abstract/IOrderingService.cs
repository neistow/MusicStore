using System.Collections.Generic;
using System.Threading.Tasks;
using Ordering.Api.Domain;

namespace Ordering.Api.Application.Abstract
{
    public interface IOrderingService
    {
        Task<List<Checkout>> GetCustomerCheckouts(string customerId);
    }
}