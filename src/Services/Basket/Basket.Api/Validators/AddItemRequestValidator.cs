using Basket.Api.Requests;
using FluentValidation;

namespace Basket.Api.Validators
{
    public class AddItemRequestValidator : AbstractValidator<AddItemRequest>
    {
        public AddItemRequestValidator()
        {
            RuleFor(r => r.ItemId).GreaterThanOrEqualTo(1);
            RuleFor(r => r.Quantity).GreaterThanOrEqualTo(1);
        }
    }
}