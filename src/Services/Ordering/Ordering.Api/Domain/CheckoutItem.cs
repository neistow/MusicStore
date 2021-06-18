namespace Ordering.Api.Domain
{
    public class CheckoutItem
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public double PricePerUnit { get; set; }

        public int CheckoutId { get; set; }
        public Checkout Checkout { get; set; }
    }
}