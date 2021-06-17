namespace Catalog.Domain.Entities
{
    public class Album
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string CoverUrl { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}