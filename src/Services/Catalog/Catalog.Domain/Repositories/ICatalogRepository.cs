using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Domain.Entities;

namespace Catalog.Domain.Repositories
{
    public interface ICatalogRepository : IRepository
    {
        Task<List<Album>> GetAlbums();
        Task<Album> GetAlbumById(int id);
        Task CreateAlbum(Album album);
        Task DeleteAlbum(Album album);
        Task<List<Genre>> GetGenres();
        Task<Genre> GetGenreById(int id);
        Task CreateGenre(Genre genre);
        Task DeleteGenre(Genre genre);
    }
}