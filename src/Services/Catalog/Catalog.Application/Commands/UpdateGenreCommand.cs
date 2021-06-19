using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using AutoMapper;
using Catalog.Application.DTO;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Commands
{
    public class UpdateGenreCommand : IRequest<GenreDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UpdateGenreCommandHandler : IRequestHandler<UpdateGenreCommand, GenreDto>
    {
        private readonly ICatalogRepository _catalogRepository;
        private readonly IMapper _mapper;

        public UpdateGenreCommandHandler(ICatalogRepository catalogRepository, IMapper mapper)
        {
            _catalogRepository = catalogRepository;
            _mapper = mapper;
        }

        public async Task<GenreDto> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
        {
            var genreInDb = await _catalogRepository.GetGenreById(request.Id);
            Guard.Against.Null(genreInDb, nameof(request.Id), "Genre not found");

            genreInDb.Name = request.Name;
            await _catalogRepository.UnitOfWork.SaveChangesAsync();

            return _mapper.Map<GenreDto>(genreInDb);
        }
    }
}