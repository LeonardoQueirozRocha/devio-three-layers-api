using Bogus;
using DevIO.Business.Entities;
using DevIO.Util.Tests.Builders.Base;

namespace DevIO.Util.Tests.Builders.Business.Entities;

public class AddressBuilder : LazyFakerBuilder<Address>
{
    private AddressBuilder()
    {
    }

    public static AddressBuilder Instance => new();

    protected override Faker<Address> Factory() =>
        new Faker<Address>(Locale)
            .RuleFor(property => property.Id, setter => setter.Random.Guid())
            .RuleFor(property => property.Street, setter => setter.Address.StreetName())
            .RuleFor(property => property.Number, setter => setter.Random.Int(1, 999).ToString())
            .RuleFor(property => property.Complement, setter => setter.Address.Direction())
            .RuleFor(property => property.ZipCode, setter => setter.Address.ZipCode("########"))
            .RuleFor(property => property.Neighborhood, setter => setter.Address.StreetAddress())
            .RuleFor(property => property.City, setter => setter.Address.City())
            .RuleFor(property => property.State, setter => setter.Address.State());
}