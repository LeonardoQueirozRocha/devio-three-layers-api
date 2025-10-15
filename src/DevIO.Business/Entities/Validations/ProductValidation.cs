using DevIO.Business.Constants;
using FluentValidation;

namespace DevIO.Business.Entities.Validations;

public class ProductValidation : AbstractValidator<Product>
{
    public ProductValidation()
    {
        RuleFor(product => product.Name)
            .NotEmpty()
            .WithMessage(ValidationMessage.NotEmptyMessage)
            .Length(2, 200)
            .WithMessage(ValidationMessage.LengthMinMaxMessage);

        RuleFor(product => product.Description)
            .NotEmpty()
            .WithMessage(ValidationMessage.NotEmptyMessage)
            .Length(2, 1000)
            .WithMessage(ValidationMessage.LengthMinMaxMessage);

        RuleFor(product => product.Value)
            .GreaterThan(decimal.Zero)
            .WithMessage(ValidationMessage.GreaterThanMessage);
    }
}