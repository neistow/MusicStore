using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Application.DTO;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Queries
{
    public class GetGenresQuery : IRequest<IEnumerable<GenreDto>>
    {
    }

    public class GetGenresQueryHandler : IRequestHandler<GetGenresQuery, IEnumerable<GenreDto>>
    {
        private readonly ICatalogRepository _catalogRepository;
        private readonly IMapper _mapper;

        public GetGenresQueryHandler(ICatalogRepository catalogRepository, IMapper mapper)
        {
            _catalogRepository = catalogRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GenreDto>> Handle(GetGenresQuery request, CancellationToken cancellationToken)
        {
            var genres = await _catalogRepository.GetGenres();
            return _mapper.Map<IEnumerable<GenreDto>>(genres);
        }
    }
}