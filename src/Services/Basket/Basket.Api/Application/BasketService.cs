using System;
using System.Linq;
using System.Threading.Tasks;
using Basket.Api.Application.Abstract;
using Basket.Api.Domain;
using Basket.Api.Domain.Abstract;
using EasyNetQ;
using GrpcCatalogClient;
using Shared.IntegrationEvents;

namespace Basket.Api.Application
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IBus _bus;
        private readonly Catalog.CatalogClient _catalogClient;

        public BasketService(IBasketRepository basketRepository, IBus bus, Catalog.CatalogClient catalogClient)
        {
            _basketRepository = basketRepository;
            _bus = bus;
            _catalogClient = catalogClient;
        }

        public async Task<Domain.Basket> GetBasket(string customerId)
        {
            var basketInDb = await _basketRepository.GetBasket(customerId);
            return basketInDb ?? await CreateBasket(customerId);
        }

        public async Task AddItem(string customerId, int itemId, int quantity)
        {
            var basket = await _basketRepository.GetBasket(customerId);
            basket ??= await CreateBasket(customerId);

            var itemInBasket = basket.Items.FirstOrDefault(i => i.ItemId == itemId);
            if (itemInBasket != null)
            {
                itemInBasket.Quantity += quantity;
            }
            else
            {
                var item = await _catalogClient.GetCatalogItemByIdAsync(new CatalogItemRequest
                {
                    Id = itemId
                });

                basket.Items.Add(new BasketItem
                {
                    ItemId = item.Id,
                    ItemName = item.Name,
                    Quantity = quantity,
                    PricePerUnit = item.Price,
                    CoverUrl = item.CoverUrl
                });
            }

            await _basketRepository.UnitOfWork.SaveChangesAsync();
        }

        public async Task RemoveItem(string customerId, int itemId, int quantity)
        {
            var basket = await _basketRepository.GetBasket(customerId);

            var basketItem = basket?.Items.FirstOrDefault(i => i.ItemId == itemId);
            if (basketItem == null)
            {
                return;
            }

            if (quantity >= basketItem.Quantity)
            {
                basket.Items.Remove(basketItem);
            }
            else
            {
                basketItem.Quantity -= quantity;
            }

            await _basketRepository.UnitOfWork.SaveChangesAsync();
        }

        public async Task Checkout(string customerId)
        {
            var basket = await _basketRepository.GetBasket(customerId);
            if (!basket.Items.Any())
            {
                throw new InvalidOperationException("Basket is empty");
            }

            var checkoutEvent = new BasketCheckoutEvent
            {
                CustomerId = customerId,
                Items = basket.Items.Select(i => new Item
                {
                    Id = i.ItemId,
                    Quantity = i.Quantity,
                    PricePerUnit = i.PricePerUnit
                }).ToList()
            };

            await _bus.PubSub.PublishAsync(checkoutEvent);

            basket.Items.Clear();
            await _basketRepository.UnitOfWork.SaveChangesAsync();
        }

        private async Task<Domain.Basket> CreateBasket(string customerId)
        {
            var basket = new Domain.Basket
            {
                CustomerId = customerId
            };

            await _basketRepository.CreateBasket(basket);
            await _basketRepository.UnitOfWork.SaveChangesAsync();

            return basket;
        }
    }
}