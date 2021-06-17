using System.Threading.Tasks;
using Catalog.Application.Queries;
using Grpc.Core;
using GrpcCatalog;
using MediatR;

namespace Catalog.Api.Grpc
{
    public class CatalogGrpcService : GrpcCatalog.Catalog.CatalogBase
    {
        private readonly IMediator _mediator;

        public CatalogGrpcService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<CatalogItemResponse> GetCatalogItemById(CatalogItemRequest request, ServerCallContext context)
        {
            var album = await _mediator.Send(new GetAlbumQuery
            {
                Id = request.Id
            });

            if (album != null)
            {
                context.Status = new Status(StatusCode.OK, "Ok");

                return new CatalogItemResponse
                {
                    Id = album.Id,
                    Name = album.Name,
                    Price = album.Price,
                    CoverUrl = album.CoverUrl ?? ""
                };
            }

            context.Status = new Status(StatusCode.NotFound, "Item not found");
            return new CatalogItemResponse();
        }
    }
}