namespace Catalog.Api.Requests
{
    public class AlbumRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int GenreId { get; set; }
    }
}