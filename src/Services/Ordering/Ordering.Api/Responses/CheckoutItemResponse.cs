namespace Ordering.Api.Responses
{
    public class CheckoutItemResponse
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public double PricePerUnit { get; set; }
    }
}