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
}