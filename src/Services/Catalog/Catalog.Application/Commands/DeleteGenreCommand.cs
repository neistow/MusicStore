using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Commands
{
    public class DeleteGenreCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteGenreCommandHandler : AsyncRequestHandler<DeleteGenreCommand>
    {
        private readonly ICatalogRepository _catalogRepository;

        public DeleteGenreCommandHandler(ICatalogRepository catalogRepository)
        {
            _catalogRepository = catalogRepository;
        }

        protected override async Task Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
        {
            var genreInDb = await _catalogRepository.GetGenreById(request.Id);
            Guard.Against.Null(genreInDb, nameof(request.Id), "Genre not found");

            await _catalogRepository.DeleteGenre(genreInDb);
            await _catalogRepository.UnitOfWork.SaveChangesAsync();
        }
    }
}