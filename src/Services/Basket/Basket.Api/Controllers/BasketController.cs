using System.Threading.Tasks;
using AutoMapper;
using Basket.Api.Application.Abstract;
using Basket.Api.Requests;
using Basket.Api.Responses;
using Microsoft.AspNetCore.Mvc;
using Shared.Abstractions;
using Shared.Hosting.Infrastructure;

namespace Basket.Api.Controllers
{
    public class BasketController : ApiControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly ICurrentUser _currentUser;
        private readonly IMapper _mapper;

        public BasketController(IBasketService basketService, ICurrentUser currentUser, IMapper mapper)
        {
            _basketService = basketService;
            _currentUser = currentUser;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket()
        {
            var basket = await _basketService.GetBasket(_currentUser.Id);
            return Ok(_mapper.Map<BasketResponse>(basket));
        }


        [HttpPost("items/add")]
        public async Task<IActionResult> AddItem(AddItemRequest request)
        {
            await _basketService.AddItem(_currentUser.Id, request.ItemId, request.Quantity);
            return Ok();
        }

        [HttpPost("items/remove")]
        public async Task<IActionResult> RemoveItem(RemoveItemRequest request)
        {
            await _basketService.RemoveItem(_currentUser.Id, request.ItemId, request.Quantity);
            return Ok();
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout()
        {
            await _basketService.Checkout(_currentUser.Id);
            return Ok();
        }
    }
}