using DevIO.Business.Constants;
using DevIO.Business.Entities.Validations.Documents;
using FluentValidation;

namespace DevIO.Business.Entities.Validations;

public class SupplierValidation : AbstractValidator<Supplier>
{
    public SupplierValidation()
    {
        RuleFor(supplier => supplier.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(ValidationMessage.NotEmptyMessage)
            .Length(2, 100)
            .WithMessage(ValidationMessage.LengthMessage);

        When(supplier => supplier.IsNaturalPerson, () =>
        {
            RuleFor(supplier => supplier.Document!.Length)
                .Cascade(CascadeMode.Stop)
                .Equal(CpfValidation.CpfSize)
                .WithMessage(ValidationMessage.EqualMessage);

            RuleFor(supplier => CpfValidation.Validate(supplier.Document!))
                .Cascade(CascadeMode.Stop)
                .Equal(true)
                .WithMessage(ValidationMessage.InvalidDocumentMessage);
        });
        
        When(supplier => supplier.IsLegalEntity, () =>
        {
            RuleFor(supplier => supplier.Document!.Length)
                .Cascade(CascadeMode.Stop)
                .Equal(CnpjValidation.CnpjSize)
                .WithMessage(ValidationMessage.EqualMessage);

            RuleFor(supplier => CnpjValidation.Validate(supplier.Document!))
                .Cascade(CascadeMode.Stop)
                .Equal(true)
                .WithMessage(ValidationMessage.InvalidDocumentMessage);
        });
    }
}