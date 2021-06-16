using Catalog.Api.Requests;
using FluentValidation;

namespace Catalog.Api.Validators
{
    public class AlbumRequestValidator : AbstractValidator<AlbumRequest>
    {
        public AlbumRequestValidator()
        {
            RuleFor(r => r.Name).NotEmpty().MaximumLength(256);
            RuleFor(r => r.Description).NotEmpty().MaximumLength(1024);
            RuleFor(r => r.Price).GreaterThanOrEqualTo(1);
            RuleFor(r => r.GenreId).GreaterThanOrEqualTo(1);
        }
    }
}