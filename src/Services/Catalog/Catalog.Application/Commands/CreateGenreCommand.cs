using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Application.DTO;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Commands
{
    public class CreateGenreCommand : IRequest<GenreDto>
    {
        public string Name { get; set; }
    }

    public class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand, GenreDto>
    {
        private readonly ICatalogRepository _catalogRepository;
        private readonly IMapper _mapper;

        public CreateGenreCommandHandler(ICatalogRepository catalogRepository, IMapper mapper)
        {
            _catalogRepository = catalogRepository;
            _mapper = mapper;
        }

        public async Task<GenreDto> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = new Genre {Name = request.Name};

            await _catalogRepository.CreateGenre(genre);
            await _catalogRepository.UnitOfWork.SaveChangesAsync();

            return _mapper.Map<GenreDto>(genre);
        }
    }
}