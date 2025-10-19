using Bogus;
using DevIO.Business.Entities;
using DevIO.Business.Entities.Validations;
using DevIO.Util.Tests.Builders.Business.Entities;
using DevIO.Util.Tests.Extensions;

namespace DevIO.Business.Tests.Entities.Validations;

public class AddressValidationTests
{
    private const string ClassName = nameof(AddressValidationTests);
    private readonly Address _address = AddressBuilder.Instance.Build();
    private readonly AddressValidation _addressValidation = new();
    private readonly Faker _faker = new("pt_BR");

    #region Success

    [Fact(DisplayName = $"{ClassName} success")]
    private void Success()
    {
        // Arrange && Act
        var result = _addressValidation.Validate(_address);

        // Assert
        result.AssertValid();
    }

    #endregion

    #region Street

    [Fact(DisplayName = $"{ClassName} {nameof(Address.Street)} should be invalid when is empty")]
    public void Street_ShouldBeInvalid_WhenIsEmpty()
    {
        // Arrange
        const string expectedErrorCode = "NotEmptyValidator";
        const string expectedErrorMessage = "The Street field needs to be informed";

        _address.Street = string.Empty;

        // Act
        var result = _addressValidation.Validate(_address);

        // Assert
        result.AssertInvalid(expectedErrorCode, expectedErrorMessage);
    }

    [Theory(DisplayName = $"{ClassName} {nameof(Address.Street)} should be invalid when length is invalid")]
    [InlineData(1)]
    [InlineData(201)]
    public void Street_ShouldBeInvalid_WhenLengthIsInvalid(int length)
    {
        // Arrange
        const string expectedErrorCode = "LengthValidator";
        const string expectedErrorMessage = "The Street field needs to have between 2 and 200 characters";

        _address.Street = _faker.Random.AlphaNumeric(length);

        // Act
        var result = _addressValidation.Validate(_address);

        // Assert
        result.AssertInvalid(expectedErrorCode, expectedErrorMessage);
    }

    #endregion

    #region Neighborhood

    [Fact(DisplayName = $"{ClassName} {nameof(Address.Neighborhood)} should be invalid when is empty")]
    public void Neighborhood_ShouldBeInvalid_WhenIsEmpty()
    {
        // Arrange
        const string expectedErrorCode = "NotEmptyValidator";
        const string expectedErrorMessage = "The Neighborhood field needs to be informed";

        _address.Neighborhood = string.Empty;

        // Act
        var result = _addressValidation.Validate(_address);

        // Assert
        result.AssertInvalid(expectedErrorCode, expectedErrorMessage);
    }

    [Theory(DisplayName = $"{ClassName} {nameof(Address.Neighborhood)} should be invalid when length is invalid")]
    [InlineData(1)]
    [InlineData(101)]
    public void Neighborhood_ShouldBeInvalid_WhenLengthIsInvalid(int length)
    {
        // Arrange
        const string expectedErrorCode = "LengthValidator";
        const string expectedErrorMessage = "The Neighborhood field needs to have between 2 and 100 characters";

        _address.Neighborhood = _faker.Random.AlphaNumeric(length);

        // Act
        var result = _addressValidation.Validate(_address);

        // Assert
        result.AssertInvalid(expectedErrorCode, expectedErrorMessage);
    }

    #endregion

    #region ZipCode

    [Fact(DisplayName = $"{ClassName} {nameof(Address.ZipCode)} should be invalid when is empty")]
    public void ZipCode_ShouldBeInvalid_WhenIsEmpty()
    {
        // Arrange
        const string expectedErrorCode = "NotEmptyValidator";
        const string expectedErrorMessage = "The Zip Code field needs to be informed";

        _address.ZipCode = string.Empty;

        // Act
        var result = _addressValidation.Validate(_address);

        // Assert
        result.AssertInvalid(expectedErrorCode, expectedErrorMessage);
    }

    [Fact(DisplayName =
        $"{ClassName} {nameof(Address.ZipCode)} should be invalid when length is not exactly eight characters")]
    public void ZipCode_ShouldBeInvalid_WhenLengthIsNotExactlyEightCharacters()
    {
        // Arrange
        const string expectedErrorCode = "ExactLengthValidator";
        const string expectedErrorMessage = "The Zip Code needs to have 8 characters";

        _address.ZipCode = _faker.Random.AlphaNumeric(7);

        // Act
        var result = _addressValidation.Validate(_address);

        // Assert
        result.AssertInvalid(expectedErrorCode, expectedErrorMessage);
    }

    #endregion

    #region City

    [Fact(DisplayName = $"{ClassName} {nameof(Address.City)} should be invalid when is empty")]
    public void City_ShouldBeInvalid_WhenIsEmpty()
    {
        // Arrange
        const string expectedErrorCode = "NotEmptyValidator";
        const string expectedErrorMessage = "The City field needs to be informed";

        _address.City = string.Empty;

        // Act
        var result = _addressValidation.Validate(_address);

        // Assert
        result.AssertInvalid(expectedErrorCode, expectedErrorMessage);
    }

    [Theory(DisplayName = $"{ClassName} {nameof(Address.City)} should be invalid when length is invalid")]
    [InlineData(1)]
    [InlineData(101)]
    public void City_ShouldBeInvalid_WhenLengthIsInvalid(int length)
    {
        // Arrange
        const string expectedErrorCode = "LengthValidator";
        const string expectedErrorMessage = "The City field needs to have between 2 and 100 characters";

        _address.City = _faker.Random.AlphaNumeric(length);

        // Act
        var result = _addressValidation.Validate(_address);

        // Assert
        result.AssertInvalid(expectedErrorCode, expectedErrorMessage);
    }

    #endregion

    #region State

    [Fact(DisplayName = $"{ClassName} {nameof(Address.State)} should be invalid when is empty")]
    public void State_ShouldBeInvalid_WhenIsEmpty()
    {
        // Arrange
        const string expectedErrorCode = "NotEmptyValidator";
        const string expectedErrorMessage = "The State field needs to be informed";

        _address.State = string.Empty;

        // Act
        var result = _addressValidation.Validate(_address);

        // Assert
        result.AssertInvalid(expectedErrorCode, expectedErrorMessage);
    }

    [Theory(DisplayName = $"{ClassName} {nameof(Address.State)} should be invalid when length is invalid")]
    [InlineData(1)]
    [InlineData(51)]
    public void State_ShouldBeInvalid_WhenLengthIsInvalid(int length)
    {
        // Arrange
        const string expectedErrorCode = "LengthValidator";
        const string expectedErrorMessage = "The State field needs to have between 2 and 50 characters";

        _address.State = _faker.Random.AlphaNumeric(length);

        // Act
        var result = _addressValidation.Validate(_address);

        // Assert
        result.AssertInvalid(expectedErrorCode, expectedErrorMessage);
    }

    #endregion

    #region Number

    [Fact(DisplayName = $"{ClassName} {nameof(Address.Number)} should be invalid when is empty")]
    public void Number_ShouldBeInvalid_WhenIsEmpty()
    {
        // Arrange
        const string expectedErrorCode = "NotEmptyValidator";
        const string expectedErrorMessage = "The Number field needs to be informed";

        _address.Number = string.Empty;

        // Act
        var result = _addressValidation.Validate(_address);

        // Assert
        result.AssertInvalid(expectedErrorCode, expectedErrorMessage);
    }

    [Fact(DisplayName = $"{ClassName} {nameof(Address.Number)} should be invalid when length is invalid")]
    public void Number_ShouldBeInvalid_WhenLengthIsInvalid()
    {
        // Arrange
        const string expectedErrorCode = "LengthValidator";
        const string expectedErrorMessage = "The Number field needs to have between 1 and 50 characters";

        _address.Number = _faker.Random.AlphaNumeric(51);

        // Act
        var result = _addressValidation.Validate(_address);

        // Assert
        result.AssertInvalid(expectedErrorCode, expectedErrorMessage);
    }

    #endregion
}