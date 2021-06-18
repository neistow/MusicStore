using System.Threading.Tasks;

namespace Basket.Api.Domain
{
    public interface IBasketRepository
    {
        IUnitOfWork UnitOfWork { get; }
        Task<Basket> GetBasket(string customerId);
        Task CreateBasket(Basket basket);
        Task DeleteItem(int itemId);
        Task UpdateItemPrice(int itemId, double newPrice);
    }
}