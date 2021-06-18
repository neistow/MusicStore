using System.Collections.Generic;

namespace Basket.Api.Responses
{
    public class BasketResponse
    {
        public IEnumerable<BasketItemResponse> Items { get; set; }
        public decimal TotalPrice { get; set; }
    }
}