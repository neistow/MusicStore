using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using AutoMapper;
using Catalog.Application.DTO;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Commands
{
    public class CreateAlbumCommand : IRequest<AlbumDto>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public int GenreId { get; set; }
    }

    public class CreateAlbumCommandHandler : IRequestHandler<CreateAlbumCommand, AlbumDto>
    {
        private readonly ICatalogRepository _catalogRepository;
        private readonly IMapper _mapper;

        public CreateAlbumCommandHandler(ICatalogRepository catalogRepository, IMapper mapper)
        {
            _catalogRepository = catalogRepository;
            _mapper = mapper;
        }

        public async Task<AlbumDto> Handle(CreateAlbumCommand request, CancellationToken cancellationToken)
        {
            var genre = await _catalogRepository.GetGenreById(request.GenreId);
            Guard.Against.Null(genre, nameof(request.GenreId), "Item not found");

            var item = new Album
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Genre = genre
            };

            await _catalogRepository.CreateAlbum(item);
            await _catalogRepository.UnitOfWork.SaveChangesAsync();

            return _mapper.Map<AlbumDto>(item);
        }
    }
}