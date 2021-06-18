namespace Shared.IntegrationEvents
{
    public class ItemPriceChangedEvent
    {
        public int ItemId { get; set; }
        public double NewPrice { get; set; }
    }
}