using DevIO.Business.Entities;
using DevIO.Business.Enums;
using DevIO.Util.Tests.Builders.Business.Entities;
using FluentAssertions;

namespace DevIO.Business.Tests.Entities;

public class SupplierTests
{
    private const string ClassName = nameof(Supplier);

    private readonly Supplier _supplier = SupplierBuilder.Instance.Build();

    #region IsNaturalPerson

    [Fact(DisplayName = $"{ClassName} {nameof(Supplier.IsNaturalPerson)} should be true")]
    public void IsNaturalPerson_ShouldBeTrue()
    {
        // Arrange
        _supplier.SupplierType = SupplierType.NaturalPerson;

        // Act
        var result = _supplier.IsNaturalPerson;

        // Assert
        result.Should().BeTrue();
    }

    [Theory(DisplayName = $"{ClassName} {nameof(Supplier.IsNaturalPerson)} should be false")]
    [InlineData(SupplierType.LegalEntity)]
    [InlineData(SupplierType.None)]
    public void IsNaturalPerson_ShouldBeFalse(SupplierType supplierType)
    {
        // Arrange
        _supplier.SupplierType = supplierType;

        // Act
        var result = _supplier.IsNaturalPerson;

        // Assert
        result.Should().BeFalse();
    }

    #endregion

    #region IsLegalEntity

    [Fact(DisplayName = $"{ClassName} {nameof(Supplier.IsLegalEntity)} should be true")]
    public void IsLegalEntity_ShouldBeTrue()
    {
        // Arrange
        _supplier.SupplierType = SupplierType.LegalEntity;

        // Act
        var result = _supplier.IsLegalEntity;

        // Assert
        result.Should().BeTrue();
    }

    [Theory(DisplayName = $"{ClassName} {nameof(Supplier.IsLegalEntity)} should be false")]
    [InlineData(SupplierType.NaturalPerson)]
    [InlineData(SupplierType.None)]
    public void IsLegalEntity_ShouldBeFalse(SupplierType supplierType)
    {
        // Arrange
        _supplier.SupplierType = supplierType;

        // Act
        var result = _supplier.IsLegalEntity;

        // Assert
        result.Should().BeFalse();
    }

    #endregion
}