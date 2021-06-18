using System.Threading;
using System.Threading.Tasks;
using Basket.Api.Domain;
using Catalog.IntegrationEvents;
using EasyNetQ.AutoSubscribe;

namespace Basket.Api.IntegrationEventHandlers
{
    public class ItemDeletedEventHandler : IConsumeAsync<ItemDeletedEvent>
    {
        private readonly IBasketRepository _basketRepository;

        public ItemDeletedEventHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task ConsumeAsync(ItemDeletedEvent message, CancellationToken cancellationToken = new CancellationToken())
        {
            await _basketRepository.DeleteItem(message.ItemId);
            await _basketRepository.UnitOfWork.SaveChangesAsync();
        }
    }
}