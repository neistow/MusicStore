using System.Threading.Tasks;
using Catalog.Api.Requests;
using Catalog.Application.Commands;
using Catalog.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Hosting.Infrastructure;

namespace Catalog.Api.Controllers
{
    public class CatalogController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public CatalogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("albums/{id}")]
        public async Task<IActionResult> GetItemById(int id)
        {
            return Ok(await _mediator.Send(new GetAlbumQuery
            {
                Id = id
            }));
        }

        [HttpPost("albums")]
        public async Task<IActionResult> CreateAlbum([FromBody] AlbumRequest album)
        {
            return Ok(await _mediator.Send(new CreateAlbumCommand
            {
                Name = album.Name,
                Description = album.Description,
                Price = album.Price,
                GenreId = album.GenreId
            }));
        }

        [HttpPut("albums/{id}")]
        public async Task<IActionResult> UpdateAlbum([FromRoute] int id, [FromBody] AlbumRequest album)
        {
            return Ok(await _mediator.Send(new UpdateAlbumCommand
            {
                Id = id,
                Name = album.Name,
                Description = album.Description,
                Price = album.Price,
                GenreId = album.GenreId
            }));
        }

        [HttpDelete("albums/{id}")]
        public async Task<IActionResult> DeleteAlbum([FromRoute] int id)
        {
            return Ok(await _mediator.Send(new DeleteAlbumCommand
            {
                Id = id
            }));
        }
    }
}