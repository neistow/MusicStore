using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Application.DTO;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Queries
{
    public class GetAlbumsQuery : IRequest<IEnumerable<AlbumDto>>
    {
    }
    
    public class GetAlbumsQueryHandler : IRequestHandler<GetAlbumsQuery, IEnumerable<AlbumDto>>
    {
        private readonly ICatalogRepository _catalogRepository;
        private readonly IMapper _mapper;

        public GetAlbumsQueryHandler(ICatalogRepository catalogRepository, IMapper mapper)
        {
            _catalogRepository = catalogRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AlbumDto>> Handle(GetAlbumsQuery request, CancellationToken cancellationToken)
        {
            var albums = await _catalogRepository.GetAlbums();
            return _mapper.Map<IEnumerable<AlbumDto>>(albums);
        }
    }
}