using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Commands
{
    public class DeleteAlbumCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteAlbumCommandHandler : AsyncRequestHandler<DeleteAlbumCommand>
    {
        private readonly ICatalogRepository _catalogRepository;

        public DeleteAlbumCommandHandler(ICatalogRepository catalogRepository)
        {
            _catalogRepository = catalogRepository;
        }

        protected override async Task Handle(DeleteAlbumCommand request, CancellationToken cancellationToken)
        {
            var album = await _catalogRepository.GetAlbumById(request.Id);
            Guard.Against.Null(album, nameof(request.Id), "Item not found");

            await _catalogRepository.DeleteAlbum(album);
            await _catalogRepository.UnitOfWork.SaveChangesAsync();
        }
    }
}