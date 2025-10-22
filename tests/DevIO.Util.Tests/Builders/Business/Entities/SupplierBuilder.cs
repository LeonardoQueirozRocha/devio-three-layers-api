using Bogus;
using Bogus.Extensions.Brazil;
using DevIO.Business.Entities;
using DevIO.Business.Enums;
using DevIO.Util.Tests.Builders.Base;

namespace DevIO.Util.Tests.Builders.Business.Entities;

public class SupplierBuilder : LazyFakerBuilder<Supplier>
{
    private SupplierBuilder()
    {
    }

    public static SupplierBuilder Instance => new();

    protected override Faker<Supplier> Factory() =>
        new Faker<Supplier>(Locale)
            .RuleFor(property => property.Id, setter => setter.Random.Guid())
            .RuleFor(property => property.Name, setter => setter.Person.FirstName)
            .RuleFor(property => property.SupplierType, setter => setter.PickRandomWithout(SupplierType.None))
            .RuleFor(
                property => property.Document,
                (setter, current) =>
                    current.IsNaturalPerson
                        ? setter.Person.Cpf(false)
                        : setter.Company.Cnpj(false))
            .RuleFor(property => property.Active, setter => setter.Random.Bool())
            .RuleFor(property => property.Address, _ => AddressBuilder.Instance.Build())
            .RuleFor(property => property.Products, _ => ProductBuilder.Instance.BuildCollection());
}