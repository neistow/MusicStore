using System;
using System.Collections.Generic;
using System.Linq;

namespace Ordering.Api.Responses
{
    public class CheckoutResponse
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string CustomerId { get; set; }
        public double TotalSum => Items.Sum(i => i.Quantity * i.PricePerUnit);

        public IEnumerable<CheckoutItemResponse> Items { get; set; }
    }
}