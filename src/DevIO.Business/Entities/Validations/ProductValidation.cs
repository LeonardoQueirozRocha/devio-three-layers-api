using DevIO.Business.Constants;
using FluentValidation;

namespace DevIO.Business.Entities.Validations;

public class ProductValidation : AbstractValidator<Product>
{
    public ProductValidation()
    {
        RuleFor(product => product.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(ValidationMessages.NotEmptyMessage)
            .Length(2, 200)
            .WithMessage(ValidationMessages.LengthMessage);

        RuleFor(product => product.Description)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(ValidationMessages.NotEmptyMessage)
            .Length(2, 1000)
            .WithMessage(ValidationMessages.LengthMessage);

        RuleFor(product => product.Value)
            .Cascade(CascadeMode.Stop)
            .GreaterThan(decimal.Zero)
            .WithMessage(ValidationMessages.GreaterThanMessage);
    }
}