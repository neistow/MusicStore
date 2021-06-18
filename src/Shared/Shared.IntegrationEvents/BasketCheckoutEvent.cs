using System.Collections.Generic;

namespace Shared.IntegrationEvents
{
    public class BasketCheckoutEvent
    {
        public string CustomerId { get; set; }
        public IEnumerable<Item> Items { get; set; }
    }

    public class Item
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double PricePerUnit { get; set; }
    }
}