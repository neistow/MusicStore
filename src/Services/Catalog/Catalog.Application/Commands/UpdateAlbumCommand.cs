using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using AutoMapper;
using Catalog.Application.DTO;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Commands
{
    public class UpdateAlbumCommand : IRequest<AlbumDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public int GenreId { get; set; }
    }

    public class UpdateAlbumCommandHandler : IRequestHandler<UpdateAlbumCommand, AlbumDto>
    {
        private readonly ICatalogRepository _catalogRepository;
        private readonly IMapper _mapper;

        public UpdateAlbumCommandHandler(ICatalogRepository catalogRepository, IMapper mapper)
        {
            _catalogRepository = catalogRepository;
            _mapper = mapper;
        }

        public async Task<AlbumDto> Handle(UpdateAlbumCommand request, CancellationToken cancellationToken)
        {
            var album = await _catalogRepository.GetAlbumById(request.Id);
            Guard.Against.Null(album, nameof(request.Id), "Item not found");

            var genre = await _catalogRepository.GetGenreById(request.GenreId);
            Guard.Against.Null(genre, nameof(request.GenreId), "Item not found");

            album.Name = request.Name;
            album.Description = request.Description;
            album.Price = request.Price;
            album.Genre = genre;

            await _catalogRepository.UnitOfWork.SaveChangesAsync();

            return _mapper.Map<AlbumDto>(album);
        }
    }
}