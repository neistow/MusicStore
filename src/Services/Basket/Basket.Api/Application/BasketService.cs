using System;
using System.Linq;
using System.Threading.Tasks;
using Basket.Api.Domain;
using Grpc.Core;
using GrpcCatalogClient;
using Microsoft.Extensions.Logging;

namespace Basket.Api.Application
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly GrpcCatalogClient.Catalog.CatalogClient _catalogClient;
        private readonly ILogger<BasketService> _logger;

        public BasketService(IBasketRepository basketRepository, GrpcCatalogClient.Catalog.CatalogClient catalogClient, ILogger<BasketService> logger)
        {
            _basketRepository = basketRepository;
            _catalogClient = catalogClient;
            _logger = logger;
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

            try
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
                await _basketRepository.UnitOfWork.SaveChangesAsync();
            }
            catch (RpcException e)
            {
                _logger.LogCritical(e.Message);
                throw;
            }
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