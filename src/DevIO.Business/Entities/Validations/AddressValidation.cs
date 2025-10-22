using DevIO.Business.Constants;
using FluentValidation;

namespace DevIO.Business.Entities.Validations;

public class AddressValidation : AbstractValidator<Address>
{
    public AddressValidation()
    {
        RuleFor(address => address.Street)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(ValidationMessages.NotEmptyMessage)
            .Length(2, 200)
            .WithMessage(ValidationMessages.LengthMessage);

        RuleFor(address => address.Neighborhood)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(ValidationMessages.NotEmptyMessage)
            .Length(2, 100)
            .WithMessage(ValidationMessages.LengthMessage);

        RuleFor(address => address.ZipCode)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(ValidationMessages.NotEmptyMessage)
            .Length(8)
            .WithMessage(ValidationMessages.ExactLengthMessage);

        RuleFor(address => address.City)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(ValidationMessages.NotEmptyMessage)
            .Length(2, 100)
            .WithMessage(ValidationMessages.LengthMessage);

        RuleFor(address => address.State)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(ValidationMessages.NotEmptyMessage)
            .Length(2, 50)
            .WithMessage(ValidationMessages.LengthMessage);

        RuleFor(address => address.Number)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(ValidationMessages.NotEmptyMessage)
            .Length(1, 50)
            .WithMessage(ValidationMessages.LengthMessage);
    }
}