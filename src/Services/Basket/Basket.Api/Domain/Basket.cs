using System.Collections.Generic;
using System.Linq;

namespace Basket.Api.Domain
{
    public class Basket
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public ICollection<BasketItem> Items { get; set; } = new List<BasketItem>();

        public double TotalPrice => Items.Sum(i => i.PricePerUnit * i.Quantity);
    }
}