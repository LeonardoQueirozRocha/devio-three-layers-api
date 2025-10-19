using Bogus;
using DevIO.Business.Entities;
using DevIO.Business.Entities.Validations;
using DevIO.Util.Tests.Builders.Business.Entities;
using DevIO.Util.Tests.Extensions;

namespace DevIO.Business.Tests.Entities.Validations;

public class ProductValidationTests
{
    private const string ClassName = nameof(ProductValidation);
    private readonly Product _product = ProductBuilder.Instance.Build();
    private readonly ProductValidation _productValidation = new();
    private readonly Faker _faker = new("pt_BR");

    #region Success

    [Fact(DisplayName = $"{ClassName} success")]
    public void Success()
    {
        // Arrange && Act
        var result = _productValidation.Validate(_product);

        // Assert
        result.AssertValid();
    }

    #endregion

    #region Name

    [Fact(DisplayName = $"{ClassName} {nameof(Product.Name)} should be invalid when is empty")]
    public void Name_ShouldBeInvalid_WhenIsEmpty()
    {
        // Arrange
        const string expectedErrorCode = "NotEmptyValidator";
        const string expectedErrorMessage = "The Name field needs to be informed";

        _product.Name = string.Empty;

        // Act
        var result = _productValidation.Validate(_product);

        // Assert
        result.AssertInvalid(expectedErrorCode, expectedErrorMessage);
    }
    
    [Theory(DisplayName = $"{ClassName} {nameof(Product.Name)} should be invalid when length is invalid")]
    [InlineData(1)]
    [InlineData(201)]
    public void Name_ShouldBeInvalid_WhenLengthIsInvalid(int length)
    {
        // Arrange
        const string expectedErrorCode = "LengthValidator";
        const string expectedErrorMessage = "The Name field needs to have between 2 and 200 characters";

        _product.Name = _faker.Random.AlphaNumeric(length);

        // Act
        var result = _productValidation.Validate(_product);

        // Assert
        result.AssertInvalid(expectedErrorCode, expectedErrorMessage);
    }

    #endregion
    
    #region Description

    [Fact(DisplayName = $"{ClassName} {nameof(Product.Description)} should be invalid when is empty")]
    public void Description_ShouldBeInvalid_WhenIsEmpty()
    {
        // Arrange
        const string expectedErrorCode = "NotEmptyValidator";
        const string expectedErrorMessage = "The Description field needs to be informed";

        _product.Description = string.Empty;

        // Act
        var result = _productValidation.Validate(_product);

        // Assert
        result.AssertInvalid(expectedErrorCode, expectedErrorMessage);
    }
    
    [Theory(DisplayName = $"{ClassName} {nameof(Product.Description)} should be invalid when length is invalid")]
    [InlineData(1)]
    [InlineData(1001)]
    public void Description_ShouldBeInvalid_WhenLengthIsInvalid(int length)
    {
        // Arrange
        const string expectedErrorCode = "LengthValidator";
        const string expectedErrorMessage = "The Description field needs to have between 2 and 1000 characters";

        _product.Description = _faker.Random.AlphaNumeric(length);

        // Act
        var result = _productValidation.Validate(_product);

        // Assert
        result.AssertInvalid(expectedErrorCode, expectedErrorMessage);
    }

    #endregion
    
    #region Value

    [Fact(DisplayName = $"{ClassName} {nameof(Product.Value)} should be invalid when is lass than zero")]
    public void Value_ShouldBeInvalid_WhenIsLassThanZero()
    {
        // Arrange
        const string expectedErrorCode = "GreaterThanValidator";
        const string expectedErrorMessage = "The Value field needs to be greater than 0";

        _product.Value = -1;

        // Act
        var result = _productValidation.Validate(_product);

        // Assert
        result.AssertInvalid(expectedErrorCode, expectedErrorMessage);
    }
    
    #endregion
}