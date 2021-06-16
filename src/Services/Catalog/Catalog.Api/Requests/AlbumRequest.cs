namespace Catalog.Api.Requests
{
    public class AlbumRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int GenreId { get; set; }
    }
}