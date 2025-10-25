using System.Linq.Expressions;
using DevIO.Business.Constants;
using DevIO.Business.Entities;
using DevIO.Business.Enums;
using DevIO.Business.Interfaces;
using DevIO.Business.Interfaces.Repositories;
using DevIO.Business.Notifications;
using DevIO.Business.Services;
using DevIO.Util.Tests.Builders.Business.Entities;
using FluentAssertions;
using Moq;

namespace DevIO.Business.Tests.Services;

public class SupplierServiceTests
{
    private const string ClassName = nameof(SupplierService);

    private readonly Mock<ISupplierRepository> _repositoryMock;
    private readonly Mock<INotificator> _notificatorMock;
    private readonly SupplierService _service;
    private readonly Supplier _supplier;

    public SupplierServiceTests()
    {
        _repositoryMock = new Mock<ISupplierRepository>();
        _notificatorMock = new Mock<INotificator>();
        _service = new SupplierService(
            _repositoryMock.Object,
            _notificatorMock.Object);
        _supplier = SupplierBuilder.Instance.Build();
    }

    #region AddAsync

    [Fact(DisplayName = $"{ClassName} {nameof(SupplierService.AddAsync)} should add supplier when is valid")]
    public async Task AddAsync_ShouldAddSupplier_WhenIsValid()
    {
        // Arrange
        _repositoryMock
            .Setup(mock =>
                mock.AddAsync(It.IsAny<Supplier>()))
            .Callback((Supplier supplierCb) =>
                supplierCb.Should().BeEquivalentTo(_supplier));

        // Act
        await _service.AddAsync(_supplier);

        // Assert
        _repositoryMock.Verify(
            mock =>
                mock.AddAsync(It.IsAny<Supplier>()),
            Times.Once);

        _notificatorMock.Verify(
            mock =>
                mock.Handle(It.IsAny<Notification>()),
            Times.Never);
    }

    [Fact(DisplayName = $"{ClassName} {nameof(SupplierService.AddAsync)} should not add supplier when is invalid")]
    public async Task AddAsync_ShouldNotAddSupplier_WhenIsInvalid()
    {
        // Arrange
        string[] expectedNotifications =
        [
            "The Name field needs to be informed",
            "The document field needs to have 11 characters and 0 were provided",
            "The informed document is invalid",
            "The Street field needs to be informed",
            "The Neighborhood field needs to be informed",
            "The Zip Code field needs to be informed",
            "The City field needs to be informed",
            "The State field needs to be informed",
            "The Number field needs to be informed",
        ];

        _supplier.Name = string.Empty;
        _supplier.SupplierType = SupplierType.NaturalPerson;
        _supplier.Document = string.Empty;
        _supplier.Address!.Street = string.Empty;
        _supplier.Address!.Neighborhood = string.Empty;
        _supplier.Address!.ZipCode = string.Empty;
        _supplier.Address!.City = string.Empty;
        _supplier.Address!.State = string.Empty;
        _supplier.Address!.Number = string.Empty;

        _notificatorMock
            .Setup(mock =>
                mock.Handle(It.IsAny<Notification>()))
            .Callback((Notification notificationCb) =>
                expectedNotifications.Should().Contain(notificationCb.Message));

        // Act
        await _service.AddAsync(_supplier);

        // Assert
        _repositoryMock.Verify(
            mock =>
                mock.AddAsync(It.IsAny<Supplier>()),
            Times.Never);

        _notificatorMock.Verify(
            mock =>
                mock.Handle(It.IsAny<Notification>()),
            Times.Exactly(expectedNotifications.Length));
    }

    [Fact(DisplayName =
        $"{ClassName}" +
        $" {nameof(SupplierService.AddAsync)}" +
        $" should add notification when supplier already exists")]
    public async Task AddAsync_ShouldAddNotification_WhenSupplierAlreadyExists()
    {
        // Arrange
        _repositoryMock
            .Setup(mock =>
                mock.FindAsync(It.IsAny<Expression<Func<Supplier, bool>>>()))
            .Callback((Expression<Func<Supplier, bool>> predicateCb) =>
                predicateCb.Compile().Invoke(_supplier).Should().BeTrue())
            .ReturnsAsync([_supplier]);

        _notificatorMock
            .Setup(mock =>
                mock.Handle(It.IsAny<Notification>()))
            .Callback((Notification notificationCb) =>
                notificationCb.Message.Should().Be(SupplierValidationMessages.SupplierAlreadyExists));

        // Act
        await _service.AddAsync(_supplier);

        // Assert
        _repositoryMock.Verify(
            mock =>
                mock.AddAsync(It.IsAny<Supplier>()),
            Times.Never);

        _repositoryMock.Verify(
            mock =>
                mock.FindAsync(It.IsAny<Expression<Func<Supplier, bool>>>()),
            Times.Once);

        _notificatorMock.Verify(
            mock =>
                mock.Handle(It.IsAny<Notification>()),
            Times.Once);
    }

    #endregion

    #region UpdateAsync

    [Fact(DisplayName = $"{ClassName} {nameof(SupplierService.UpdateAsync)} should update supplier when is valid")]
    public async Task UpdateAsync_ShouldUpdateSupplier_WhenIsValid()
    {
        // Arrange
        _repositoryMock
            .Setup(mock =>
                mock.UpdateAsync(It.IsAny<Supplier>()))
            .Callback((Supplier supplierCb) =>
                supplierCb.Should().BeEquivalentTo(_supplier));

        // Act
        await _service.UpdateAsync(_supplier);

        // Assert
        _repositoryMock.Verify(
            mock =>
                mock.UpdateAsync(It.IsAny<Supplier>()),
            Times.Once);

        _notificatorMock.Verify(
            mock =>
                mock.Handle(It.IsAny<Notification>()),
            Times.Never);
    }

    [Fact(DisplayName =
        $"{ClassName}" +
        $" {nameof(SupplierService.UpdateAsync)}" +
        $" should not update supplier when is invalid")]
    public async Task UpdateAsync_ShouldNotUpdateSupplier_WhenIsInvalid()
    {
        // Arrange
        string[] expectedNotifications =
        [
            "The Name field needs to be informed",
            "The document field needs to have 11 characters and 0 were provided",
            "The informed document is invalid"
        ];

        _supplier.Name = string.Empty;
        _supplier.SupplierType = SupplierType.NaturalPerson;
        _supplier.Document = string.Empty;

        _notificatorMock
            .Setup(mock =>
                mock.Handle(It.IsAny<Notification>()))
            .Callback((Notification notificationCb) =>
                expectedNotifications.Should().Contain(notificationCb.Message));

        // Act
        await _service.UpdateAsync(_supplier);

        // Assert
        _repositoryMock.Verify(
            mock =>
                mock.UpdateAsync(It.IsAny<Supplier>()),
            Times.Never);

        _notificatorMock.Verify(
            mock =>
                mock.Handle(It.IsAny<Notification>()),
            Times.Exactly(expectedNotifications.Length));
    }

    [Fact(DisplayName =
        $"{ClassName}" +
        $" {nameof(SupplierService.UpdateAsync)}" +
        $" should add notification when supplier already exists")]
    public async Task UpdateAsync_ShouldAddNotification_WhenSupplierAlreadyExists()
    {
        // Arrange
        var existingSupplier = SupplierBuilder.Instance.Build();
        existingSupplier.Document = _supplier.Document;

        _repositoryMock
            .Setup(mock =>
                mock.FindAsync(It.IsAny<Expression<Func<Supplier, bool>>>()))
            .Callback((Expression<Func<Supplier, bool>> predicateCb) =>
                predicateCb.Compile().Invoke(existingSupplier).Should().BeTrue())
            .ReturnsAsync([existingSupplier]);

        _notificatorMock
            .Setup(mock =>
                mock.Handle(It.IsAny<Notification>()))
            .Callback((Notification notificationCb) =>
                notificationCb.Message.Should().Be(SupplierValidationMessages.SupplierAlreadyExists));

        // Act
        await _service.UpdateAsync(_supplier);

        // Assert
        _repositoryMock.Verify(
            mock =>
                mock.AddAsync(It.IsAny<Supplier>()),
            Times.Never);

        _repositoryMock.Verify(
            mock =>
                mock.FindAsync(It.IsAny<Expression<Func<Supplier, bool>>>()),
            Times.Once);

        _notificatorMock.Verify(
            mock =>
                mock.Handle(It.IsAny<Notification>()),
            Times.Once);
    }

    #endregion

    #region RemoveAsync

    [Fact(DisplayName = $"{ClassName} {nameof(SupplierService.RemoveAsync)} should remove supplier when is valid")]
    public async Task RemoveAsync_ShouldRemoveSupplier_WhenIsValid()
    {
        // Arrange
        var supplierId = Guid.NewGuid();

        _supplier.Id = supplierId;
        _supplier.Products = [];

        _repositoryMock
            .Setup(mock =>
                mock.GetSupplierWithProductsAndAddressAsync(It.IsAny<Guid>()))
            .Callback((Guid idCb) =>
                idCb.Should().Be(supplierId))
            .ReturnsAsync(_supplier);

        _repositoryMock
            .Setup(mock =>
                mock.RemoveAddressAndSupplierAsync(It.IsAny<Address>()))
            .Callback((Address addressCb) =>
                addressCb.Should().BeEquivalentTo(_supplier.Address));

        _repositoryMock
            .Setup(mock =>
                mock.RemoveAsync(It.IsAny<Guid>()))
            .Callback((Guid idCb) =>
                idCb.Should().Be(supplierId));

        // Act
        await _service.RemoveAsync(supplierId);

        // Assert
        _repositoryMock.Verify(
            mock =>
                mock.GetSupplierWithProductsAndAddressAsync(It.IsAny<Guid>()),
            Times.Once);

        _repositoryMock.Verify(
            mock =>
                mock.RemoveAddressAndSupplierAsync(It.IsAny<Address>()),
            Times.Once);

        _repositoryMock.Verify(
            mock =>
                mock.RemoveAsync(It.IsAny<Guid>()),
            Times.Once);

        _notificatorMock.Verify(
            mock =>
                mock.Handle(It.IsAny<Notification>()),
            Times.Never);
    }

    [Fact(DisplayName =
        $"{ClassName} " +
        $"{nameof(SupplierService.RemoveAsync)}" +
        $" should remove supplier when do not have address")]
    public async Task RemoveAsync_ShouldRemoveSupplier_WhenDoNotHaveAddress()
    {
        // Arrange
        var supplierId = Guid.NewGuid();

        _supplier.Id = supplierId;
        _supplier.Products = [];
        _supplier.Address = null;

        _repositoryMock
            .Setup(mock =>
                mock.GetSupplierWithProductsAndAddressAsync(It.IsAny<Guid>()))
            .Callback((Guid idCb) =>
                idCb.Should().Be(supplierId))
            .ReturnsAsync(_supplier);

        _repositoryMock
            .Setup(mock =>
                mock.RemoveAsync(It.IsAny<Guid>()))
            .Callback((Guid idCb) =>
                idCb.Should().Be(supplierId));

        // Act
        await _service.RemoveAsync(supplierId);

        // Assert
        _repositoryMock.Verify(
            mock =>
                mock.GetSupplierWithProductsAndAddressAsync(It.IsAny<Guid>()),
            Times.Once);

        _repositoryMock.Verify(
            mock =>
                mock.RemoveAddressAndSupplierAsync(It.IsAny<Address>()),
            Times.Never);

        _repositoryMock.Verify(
            mock =>
                mock.RemoveAsync(It.IsAny<Guid>()),
            Times.Once);

        _notificatorMock.Verify(
            mock =>
                mock.Handle(It.IsAny<Notification>()),
            Times.Never);
    }

    [Fact(DisplayName =
        $"{ClassName} " +
        $"{nameof(SupplierService.RemoveAsync)}" +
        $" should add notification when supplier does not exists")]
    public async Task RemoveAsync_ShouldAddNotification_WhenSupplierDoesNotExists()
    {
        // Arrange
        var supplierId = Guid.NewGuid();

        _repositoryMock
            .Setup(mock =>
                mock.GetSupplierWithProductsAndAddressAsync(It.IsAny<Guid>()))
            .Callback((Guid idCb) =>
                idCb.Should().Be(supplierId));

        _notificatorMock
            .Setup(mock =>
                mock.Handle(It.IsAny<Notification>()))
            .Callback((Notification notificationCb) =>
                notificationCb.Message.Should().BeEquivalentTo(SupplierValidationMessages.SupplierDoesNotExists));

        // Act
        await _service.RemoveAsync(supplierId);

        // Assert
        _repositoryMock.Verify(
            mock =>
                mock.GetSupplierWithProductsAndAddressAsync(It.IsAny<Guid>()),
            Times.Once);

        _repositoryMock.Verify(
            mock =>
                mock.RemoveAddressAndSupplierAsync(It.IsAny<Address>()),
            Times.Never);

        _repositoryMock.Verify(
            mock =>
                mock.RemoveAsync(It.IsAny<Guid>()),
            Times.Never);

        _notificatorMock.Verify(
            mock =>
                mock.Handle(It.IsAny<Notification>()),
            Times.Once);
    }

    [Fact(DisplayName =
        $"{ClassName} " +
        $"{nameof(SupplierService.RemoveAsync)}" +
        $" should add notification when supplier has products")]
    public async Task RemoveAsync_ShouldAddNotification_WhenSupplierHasProducts()
    {
        // Arrange
        var supplierId = Guid.NewGuid();
        _supplier.Id = supplierId;

        _repositoryMock
            .Setup(mock =>
                mock.GetSupplierWithProductsAndAddressAsync(It.IsAny<Guid>()))
            .Callback((Guid idCb) =>
                idCb.Should().Be(supplierId))
            .ReturnsAsync(_supplier);

        _notificatorMock
            .Setup(mock =>
                mock.Handle(It.IsAny<Notification>()))
            .Callback((Notification notificationCb) =>
                notificationCb.Message.Should().BeEquivalentTo(SupplierValidationMessages.SupplierHasProducts));

        // Act
        await _service.RemoveAsync(supplierId);

        // Assert
        _repositoryMock.Verify(
            mock =>
                mock.GetSupplierWithProductsAndAddressAsync(It.IsAny<Guid>()),
            Times.Once);

        _repositoryMock.Verify(
            mock =>
                mock.RemoveAddressAndSupplierAsync(It.IsAny<Address>()),
            Times.Never);

        _repositoryMock.Verify(
            mock =>
                mock.RemoveAsync(It.IsAny<Guid>()),
            Times.Never);

        _notificatorMock.Verify(
            mock =>
                mock.Handle(It.IsAny<Notification>()),
            Times.Once);
    }

    #endregion

    #region Disponse

    [Fact(DisplayName = $"{ClassName} {nameof(SupplierService.Dispose)} should dispose repository")]
    public void Dispose_ShouldDisposeRepository()
    {
        // Arrange && Act
        _service.Dispose();

        // Assert
        _repositoryMock.Verify(
            mock =>
                mock.Dispose(),
            Times.Once);
    }

    #endregion
}