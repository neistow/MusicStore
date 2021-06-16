namespace Catalog.Application.DTO
{
    public class AlbumDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string CoverUrl { get; set; }

        public int Genre { get; set; }
    }
}