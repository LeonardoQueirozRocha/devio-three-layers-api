using Bogus;
using DevIO.Business.Entities;
using DevIO.Business.Entities.Validations;
using DevIO.Business.Enums;
using DevIO.Util.Tests.Builders.Business.Entities;
using DevIO.Util.Tests.Extensions;
using FluentAssertions;

namespace DevIO.Business.Tests.Entities.Validations;

public class SupplierValidationTests
{
    private const string ClassName = nameof(SupplierValidation);
    private readonly Supplier _supplier = SupplierBuilder.Instance.Build();
    private readonly SupplierValidation _supplierValidation = new();
    private readonly Faker _faker = new("pt_BR");

    #region Success

    [Fact(DisplayName = $"{ClassName} success")]
    public void Success()
    {
        // Arrange && Act
        var result = _supplierValidation.Validate(_supplier);

        // Assert
        result.AssertValid();
    }

    #endregion

    #region Name

    [Fact(DisplayName = $"{ClassName} {nameof(Supplier.Name)} should be invalid when is empty")]
    public void Name_ShouldBeInvalid_WhenIsEmpty()
    {
        // Arrange
        const string expectedErrorCode = "NotEmptyValidator";
        const string expectedErrorMessage = "The Name field needs to be informed";

        _supplier.Name = string.Empty;

        // Act
        var result = _supplierValidation.Validate(_supplier);

        // Assert
        result.AssertInvalid(expectedErrorCode, expectedErrorMessage);
    }

    [Theory(DisplayName = $"{ClassName} {nameof(Supplier.Name)} should be invalid when length is invalid")]
    [InlineData(1)]
    [InlineData(101)]
    public void Name_ShouldBeInvalid_WhenLengthIsInvalid(int length)
    {
        // Arrange
        const string expectedErrorCode = "LengthValidator";
        const string expectedErrorMessage = "The Name field needs to have between 2 and 100 characters";

        _supplier.Name = _faker.Random.AlphaNumeric(length);

        // Act
        var result = _supplierValidation.Validate(_supplier);

        // Assert
        result.AssertInvalid(expectedErrorCode, expectedErrorMessage);
    }

    #endregion

    #region Document

    [Theory(DisplayName = $"{ClassName} {nameof(Supplier.Document)} should be invalid when document size is invalid")]
    [InlineData(SupplierType.NaturalPerson, 11, 10)]
    [InlineData(SupplierType.LegalEntity, 14, 13)]
    public void Document_ShouldBeInvalid_WhenSupplierIsNaturalPersonAndDocumentSizeIsInvalid(
        SupplierType supplierType,
        int documentSize,
        int invalidDocumentSize)
    {
        // Arrange
        (string ErroCode, string ErrorMessage)[] expectedErrors =
        [
            (
                "EqualValidator",
                $"The document field needs to have {documentSize} characters and {invalidDocumentSize} were provided"
            ),
            (
                "EqualValidator",
                "The informed document is invalid"
            )
        ];

        _supplier.SupplierType = supplierType;
        _supplier.Document = _faker.Random.AlphaNumeric(invalidDocumentSize);

        // Act
        var result = _supplierValidation.Validate(_supplier);

        // Assert
        result.AssertInvalid(expectedErrors);
    }

    #endregion
}