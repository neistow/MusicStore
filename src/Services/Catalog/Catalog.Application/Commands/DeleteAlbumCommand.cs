using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Catalog.Domain.Repositories;
using Catalog.IntegrationEvents;
using EasyNetQ;
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
        private readonly IBus _bus;

        public DeleteAlbumCommandHandler(ICatalogRepository catalogRepository, IBus bus)
        {
            _catalogRepository = catalogRepository;
            _bus = bus;
        }

        protected override async Task Handle(DeleteAlbumCommand request, CancellationToken cancellationToken)
        {
            var album = await _catalogRepository.GetAlbumById(request.Id);
            Guard.Against.Null(album, nameof(request.Id), "Item not found");

            await _catalogRepository.DeleteAlbum(album);
            await _catalogRepository.UnitOfWork.SaveChangesAsync();

            await _bus.PubSub.PublishAsync(new ItemDeletedEvent
            {
                ItemId = request.Id
            });
        }
    }
}