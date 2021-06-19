using System.Threading.Tasks;
using Catalog.Api.Requests;
using Catalog.Application.Commands;
using Catalog.Application.DTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Shared.Hosting.Infrastructure;

namespace Catalog.Api.Controllers
{
    [Authorize("AdminScope")]
    public class AlbumsController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public AlbumsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [SwaggerResponse(200, typeof(AlbumDto))]
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

        [HttpPut]
        [SwaggerResponse(200, typeof(AlbumDto))]
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

        [HttpDelete]
        public async Task<IActionResult> DeleteAlbum([FromRoute] int id)
        {
            return Ok(await _mediator.Send(new DeleteAlbumCommand
            {
                Id = id
            }));
        }
    }
}