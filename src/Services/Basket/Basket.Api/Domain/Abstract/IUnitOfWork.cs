using System.Threading;
using System.Threading.Tasks;

namespace Basket.Api.Domain.Abstract
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}