using Bogus;
using DevIO.Business.Entities;
using DevIO.Util.Tests.Builders.Base;

namespace DevIO.Util.Tests.Builders.Business.Entities;

public class ProductBuilder : LazyFakerBuilder<Product>
{
    private ProductBuilder()
    {
    }

    public static ProductBuilder Instance => new();

    protected override Faker<Product> Factory() =>
        new Faker<Product>(Locale)
            .RuleFor(property => property.Name, setter => setter.Commerce.ProductName())
            .RuleFor(property => property.Description, setter => setter.Commerce.ProductDescription())
            .RuleFor(property => property.Value, setter => decimal.Parse(setter.Commerce.Price()))
            .RuleFor(property => property.RegistrationDate, setter => setter.Date.Recent())
            .RuleFor(property => property.Active, setter => setter.Random.Bool());
}