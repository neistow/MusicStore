using System.Linq;
using System.Threading.Tasks;
using Basket.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace Basket.Api.Infrastructure
{
    public class BasketRepository : IBasketRepository
    {
        private readonly BasketContext _basketContext;

        public IUnitOfWork UnitOfWork => _basketContext;

        public BasketRepository(BasketContext basketContext)
        {
            _basketContext = basketContext;
        }

        public Task<Domain.Basket> GetBasket(string customerId)
        {
            return _basketContext.Baskets.FirstOrDefaultAsync(b => b.CustomerId == customerId);
        }

        public Task CreateBasket(Domain.Basket basket)
        {
            return _basketContext.AddAsync(basket).AsTask();
        }

        public async Task DeleteItem(int itemId)
        {
            var items = await _basketContext.BasketItems.Where(b => b.ItemId == itemId).ToListAsync();
            _basketContext.RemoveRange(items);
        }

        public async Task UpdateItemPrice(int itemId, double newPrice)
        {
            var items = await _basketContext.BasketItems.Where(b => b.ItemId == itemId).ToListAsync();
            items.ForEach(i => i.PricePerUnit = newPrice);
        }
    }
}