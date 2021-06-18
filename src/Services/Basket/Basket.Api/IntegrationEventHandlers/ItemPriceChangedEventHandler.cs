using System.Threading;
using System.Threading.Tasks;
using Basket.Api.Domain;
using Catalog.IntegrationEvents;
using EasyNetQ.AutoSubscribe;

namespace Basket.Api.IntegrationEventHandlers
{
    public class ItemPriceChangedEventHandler : IConsumeAsync<ItemPriceChangedEvent>
    {
        private readonly IBasketRepository _basketRepository;

        public ItemPriceChangedEventHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task ConsumeAsync(ItemPriceChangedEvent message, CancellationToken cancellationToken = new CancellationToken())
        {
            await _basketRepository.UpdateItemPrice(message.ItemId, message.NewPrice);
            await _basketRepository.UnitOfWork.SaveChangesAsync();
        }
    }
}