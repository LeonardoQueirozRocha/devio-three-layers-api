using DevIO.Business.Constants;
using FluentValidation;

namespace DevIO.Business.Entities.Validations;

public class AddressValidation : AbstractValidator<Address>
{
    public AddressValidation()
    {
        RuleFor(address => address.Street)
            .NotEmpty()
            .WithMessage(ValidationMessage.NotEmptyMessage)
            .Length(2, 200)
            .WithMessage(ValidationMessage.LengthMinMaxMessage);

        RuleFor(address => address.Neighborhood)
            .NotEmpty()
            .WithMessage(ValidationMessage.NotEmptyMessage)
            .Length(2, 100)
            .WithMessage(ValidationMessage.LengthMinMaxMessage);

        RuleFor(address => address.ZipCode)
            .NotEmpty()
            .WithMessage(ValidationMessage.NotEmptyMessage)
            .Length(8)
            .WithMessage(ValidationMessage.LengthMaxMessage);

        RuleFor(address => address.City)
            .NotEmpty()
            .WithMessage(ValidationMessage.NotEmptyMessage)
            .Length(2, 100)
            .WithMessage(ValidationMessage.LengthMinMaxMessage);

        RuleFor(address => address.State)
            .NotEmpty()
            .WithMessage(ValidationMessage.NotEmptyMessage)
            .Length(2, 50)
            .WithMessage(ValidationMessage.LengthMinMaxMessage);

        RuleFor(address => address.Number)
            .NotEmpty()
            .WithMessage(ValidationMessage.NotEmptyMessage)
            .Length(1, 50)
            .WithMessage(ValidationMessage.LengthMinMaxMessage);
    }
}