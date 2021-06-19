using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Api.Requests;
using Catalog.Application.Commands;
using Catalog.Application.DTO;
using Catalog.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Shared.Hosting.Infrastructure;

namespace Catalog.Api.Controllers
{
    [AllowAnonymous]
    public class CatalogController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public CatalogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("albums")]
        [SwaggerResponse(200, typeof(IEnumerable<AlbumDto>))]
        public async Task<IActionResult> GetAlbums()
        {
            return Ok(await _mediator.Send(new GetAlbumsQuery()));
        }

        [HttpGet("albums/{id}")]
        [SwaggerResponse(200, typeof(AlbumDto))]
        public async Task<IActionResult> GetAlbumsById(int id)
        {
            return Ok(await _mediator.Send(new GetAlbumQuery
            {
                Id = id
            }));
        }

        [HttpGet("genres")]
        [SwaggerResponse(200, typeof(IEnumerable<GenreDto>))]
        public async Task<IActionResult> GetGenres()
        {
            return Ok(await _mediator.Send(new GetGenresQuery()));
        }
    }
}