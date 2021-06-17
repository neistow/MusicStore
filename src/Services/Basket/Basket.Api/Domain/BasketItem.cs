namespace Basket.Api.Domain
{
    public class BasketItem
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public double PricePerUnit { get; set; }
        public int Quantity { get; set; }
        public string CoverUrl { get; set; }
    }
}