using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using AutoMapper;
using Catalog.Application.DTO;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Queries
{
    public class GetAlbumQuery : IRequest<AlbumDto>
    {
        public int Id { get; set; }
    }

    public class GetAlbumQueryHandler : IRequestHandler<GetAlbumQuery, AlbumDto>
    {
        private readonly ICatalogRepository _catalogRepository;
        private readonly IMapper _mapper;

        public GetAlbumQueryHandler(ICatalogRepository catalogRepository, IMapper mapper)
        {
            _catalogRepository = catalogRepository;
            _mapper = mapper;
        }

        public async Task<AlbumDto> Handle(GetAlbumQuery request, CancellationToken cancellationToken)
        {
            var album = await _catalogRepository.GetAlbumById(request.Id);
            Guard.Against.Null(album, nameof(request.Id), "Item not found");

            return _mapper.Map<AlbumDto>(album);
        }
    }
}