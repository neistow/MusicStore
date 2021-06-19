using System.Threading.Tasks;
using Catalog.Api.Requests;
using Catalog.Application.Commands;
using Catalog.Application.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Shared.Hosting.Infrastructure;

namespace Catalog.Api.Controllers
{
    public class GenreController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public GenreController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [SwaggerResponse(200, typeof(GenreDto))]
        public async Task<IActionResult> CreateGenre([FromBody] GenreRequest request)
        {
            return Ok(await _mediator.Send(new CreateGenreCommand
            {
                Name = request.Name
            }));
        }

        [HttpPut("{id}")]
        [SwaggerResponse(200, typeof(GenreDto))]
        public async Task<IActionResult> UpdateGenre([FromRoute] int id, [FromBody] GenreRequest request)
        {
            return Ok(await _mediator.Send(new UpdateGenreCommand
            {
                Id = id,
                Name = request.Name
            }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre([FromRoute] int id)
        {
            return Ok(await _mediator.Send(new DeleteGenreCommand
            {
                Id = id
            }));
        }
    }
}