namespace Basket.Api.Responses
{
    public class BasketItemResponse
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal PricePerUnit { get; set; }
        public int Quantity { get; set; }
        public string CoverUrl { get; set; }
    }
}