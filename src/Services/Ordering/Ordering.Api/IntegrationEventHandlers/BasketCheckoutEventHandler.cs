using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;
using Ordering.Api.Domain;
using Ordering.Api.Domain.Abstract;
using Shared.IntegrationEvents;

namespace Ordering.Api.IntegrationEventHandlers
{
    public class BasketCheckoutEventHandler : IConsumeAsync<BasketCheckoutEvent>
    {
        private readonly IOrderingRepository _orderingRepository;

        public BasketCheckoutEventHandler(IOrderingRepository orderingRepository)
        {
            _orderingRepository = orderingRepository;
        }

        public async Task ConsumeAsync(BasketCheckoutEvent message, CancellationToken cancellationToken = new CancellationToken())
        {
            var checkout = new Checkout
            {
                Date = DateTime.UtcNow,
                CustomerId = message.CustomerId,
                Items = message.Items.Select(i => new CheckoutItem
                {
                    ItemId = i.Id,
                    PricePerUnit = i.PricePerUnit,
                    Quantity = i.Quantity
                }).ToList()
            };

            await _orderingRepository.CreateCheckout(checkout);
            await _orderingRepository.UnitOfWork.SaveChangesAsync();
        }
    }
}