using System.Threading.Tasks;
using Catalog.Domain.Entities;

namespace Catalog.Domain.Repositories
{
    public interface ICatalogRepository : IRepository
    {
        Task<Album> GetAlbumById(int id);
        Task CreateAlbum(Album album);
        Task DeleteAlbum(Album album);
        Task<Genre> GetGenreById(int id);
        Task CreateGenre(Genre genre);
        Task DeleteGenre(Genre genre);
    }
}