using Catalog.Api.Requests;
using FluentValidation;

namespace Catalog.Api.Validators
{
    public class GenreRequestValidator : AbstractValidator<GenreRequest>
    {
        public GenreRequestValidator()
        {
            RuleFor(g => g.Name).MinimumLength(3).MaximumLength(256);
        }
    }
}