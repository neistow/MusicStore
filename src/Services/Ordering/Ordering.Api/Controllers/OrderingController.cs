using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ordering.Api.Application.Abstract;
using Ordering.Api.Responses;
using Shared.Abstractions;
using Shared.Hosting.Infrastructure;

namespace Ordering.Api.Controllers
{
    public class OrderingController : ApiControllerBase
    {
        private readonly IOrderingService _orderingService;
        private readonly ICurrentUser _currentUser;
        private readonly IMapper _mapper;

        public OrderingController(IOrderingService orderingService, ICurrentUser currentUser, IMapper mapper)
        {
            _orderingService = orderingService;
            _currentUser = currentUser;
            _mapper = mapper;
        }

        [HttpGet("orders")]
        public async Task<IActionResult> GetCheckouts()
        {
            var checkouts = await _orderingService.GetCustomerCheckouts(_currentUser.Id);
            return Ok(_mapper.Map<List<CheckoutResponse>>(checkouts));
        }
    }
}