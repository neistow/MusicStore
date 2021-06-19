using System.Threading.Tasks;
using Catalog.Domain.Repositories;
using Grpc.Core;
using GrpcCatalogServer;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.Api.Grpc
{
    [Authorize("GrpcScope")]
    public class CatalogGrpcService : GrpcCatalogServer.Catalog.CatalogBase
    {
        private readonly ICatalogRepository _catalogRepository;

        public CatalogGrpcService(ICatalogRepository catalogRepository)
        {
            _catalogRepository = catalogRepository;
        }

        public override async Task<CatalogItemResponse> GetCatalogItemById(CatalogItemRequest request, ServerCallContext context)
        {
            var album = await _catalogRepository.GetAlbumById(request.Id);
            if (album == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Item not found"));
            }

            context.Status = new Status(StatusCode.OK, "Ok");
            return new CatalogItemResponse
            {
                Id = album.Id,
                Name = album.Name,
                Price = album.Price,
                CoverUrl = album.CoverUrl ?? ""
            };
        }
    }
}