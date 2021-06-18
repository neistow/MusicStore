using System.Threading.Tasks;

namespace Basket.Api.Application.Abstract
{
    public interface IBasketService
    {
        Task<Domain.Basket> GetBasket(string customerId);
        Task AddItem(string customerId, int itemId, int quantity);
        Task RemoveItem(string customerId, int itemId, int quantity);
    }
}