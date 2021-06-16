using System.Threading.Tasks;
using Catalog.Domain;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;

namespace Catalog.Infrastructure.Repositories
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly CatalogContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public CatalogRepository(CatalogContext context)
        {
            _context = context;
        }

        public Task<Album> GetAlbumById(int id)
        {
            return _context.Albums.FindAsync(id).AsTask();
        }

        public Task CreateAlbum(Album album)
        {
            return _context.Albums.AddAsync(album).AsTask();
        }

        public Task DeleteAlbum(Album album)
        {
            _context.Albums.Remove(album);
            return Task.CompletedTask;
        }

        public Task<Genre> GetGenreById(int id)
        {
            return _context.Genres.FindAsync(id).AsTask();
        }

        public Task CreateGenre(Genre genre)
        {
            return _context.Genres.AddAsync(genre).AsTask();
        }

        public Task DeleteGenre(Genre genre)
        {
            _context.Genres.Remove(genre);
            return Task.CompletedTask;
        }
    }
}