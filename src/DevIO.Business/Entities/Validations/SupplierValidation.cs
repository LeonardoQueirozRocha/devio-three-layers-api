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
            .WithMessage(ValidationMessages.NotEmptyMessage)
            .Length(2, 100)
            .WithMessage(ValidationMessages.LengthMessage);

        When(supplier => supplier.IsNaturalPerson, () =>
        {
            RuleFor(supplier => supplier.Document!.Length)
                .Cascade(CascadeMode.Stop)
                .Equal(CpfValidation.CpfSize)
                .WithMessage(ValidationMessages.EqualMessage);

            RuleFor(supplier => CpfValidation.Validate(supplier.Document!))
                .Cascade(CascadeMode.Stop)
                .Equal(true)
                .WithMessage(ValidationMessages.InvalidDocumentMessage);
        });
        
        When(supplier => supplier.IsLegalEntity, () =>
        {
            RuleFor(supplier => supplier.Document!.Length)
                .Cascade(CascadeMode.Stop)
                .Equal(CnpjValidation.CnpjSize)
                .WithMessage(ValidationMessages.EqualMessage);

            RuleFor(supplier => CnpjValidation.Validate(supplier.Document!))
                .Cascade(CascadeMode.Stop)
                .Equal(true)
                .WithMessage(ValidationMessages.InvalidDocumentMessage);
        });
    }
}