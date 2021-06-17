namespace Basket.Api.Requests
{
    public class RemoveItemRequest
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
    }
}