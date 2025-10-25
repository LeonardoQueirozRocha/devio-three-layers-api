using DevIO.Business.Entities;
using DevIO.Business.Interfaces;
using DevIO.Business.Interfaces.Repositories;
using DevIO.Business.Notifications;
using DevIO.Business.Services;
using DevIO.Util.Tests.Builders.Business.Entities;
using FluentAssertions;
using Moq;

namespace DevIO.Business.Tests.Services;

public class ProductServiceTests
{
    private const string ClassName = nameof(ProductService);

    private readonly Mock<IProductRepository> _repositoryMock;
    private readonly Mock<INotificator> _notificatorMock;
    private readonly ProductService _service;
    private readonly Product _product;

    public ProductServiceTests()
    {
        _repositoryMock = new Mock<IProductRepository>();
        _notificatorMock = new Mock<INotificator>();
        _service = new ProductService(
            _repositoryMock.Object,
            _notificatorMock.Object);
        _product = ProductBuilder.Instance.Build();
    }

    #region AddAsync

    [Fact(DisplayName = $"{ClassName} {nameof(ProductService.AddAsync)} should add product when is valid")]
    public async Task AddAsync_ShouldAddProduct_WhenIsValid()
    {
        // Arrange
        _repositoryMock
            .Setup(mock =>
                mock.AddAsync(It.IsAny<Product>()))
            .Callback((Product productCb) =>
                productCb.Should().BeEquivalentTo(_product));

        // Act
        await _service.AddAsync(_product);

        // Assert
        _repositoryMock.Verify(
            mock =>
                mock.AddAsync(It.IsAny<Product>()),
            Times.Once);

        _notificatorMock.Verify(
            mock =>
                mock.Handle(It.IsAny<Notification>()),
            Times.Never);
    }

    [Fact(DisplayName =
        $"{ClassName} {nameof(ProductService.AddAsync)} should add notifications when product is invalid")]
    public async Task AddAsync_ShouldAddNotifications_WhenProductIsInvalid()
    {
        // Arrange
        string[] expectedNotifications =
        [
            "The Name field needs to be informed",
            "The Description field needs to be informed",
            "The Value field needs to be greater than 0"
        ];

        _product.Name = string.Empty;
        _product.Description = string.Empty;
        _product.Value = decimal.Zero;

        _notificatorMock
            .Setup(mock =>
                mock.Handle(It.IsAny<Notification>()))
            .Callback((Notification notificationCb) =>
                expectedNotifications.Should().Contain(notificationCb.Message));

        // Act
        await _service.AddAsync(_product);

        // Assert
        _repositoryMock.Verify(
            mock =>
                mock.AddAsync(It.IsAny<Product>()),
            Times.Never);

        _notificatorMock.Verify(
            mock =>
                mock.Handle(It.IsAny<Notification>()),
            Times.Exactly(expectedNotifications.Length));
    }

    #endregion

    #region UpdateAsync

    [Fact(DisplayName = $"{ClassName} {nameof(ProductService.UpdateAsync)} should update product when is valid")]
    public async Task UpdateAsync_ShouldUpdateProduct_WhenIsValid()
    {
        // Arrange
        _repositoryMock
            .Setup(mock =>
                mock.UpdateAsync(It.IsAny<Product>()))
            .Callback((Product productCb) =>
                productCb.Should().BeEquivalentTo(_product));

        // Act
        await _service.UpdateAsync(_product);

        // Assert
        _repositoryMock.Verify(
            mock =>
                mock.UpdateAsync(It.IsAny<Product>()),
            Times.Once);

        _notificatorMock.Verify(
            mock =>
                mock.Handle(It.IsAny<Notification>()),
            Times.Never);
    }

    [Fact(DisplayName =
        $"{ClassName} {nameof(ProductService.UpdateAsync)} should add notifications when product is invalid")]
    public async Task UpdateAsync_ShouldAddNotifications_WhenProductIsInvalid()
    {
        // Arrange
        string[] expectedNotifications =
        [
            "The Name field needs to be informed",
            "The Description field needs to be informed",
            "The Value field needs to be greater than 0"
        ];

        _product.Name = string.Empty;
        _product.Description = string.Empty;
        _product.Value = decimal.Zero;

        _notificatorMock
            .Setup(mock =>
                mock.Handle(It.IsAny<Notification>()))
            .Callback((Notification notificationCb) =>
                expectedNotifications.Should().Contain(notificationCb.Message));

        // Act
        await _service.UpdateAsync(_product);

        // Assert
        _repositoryMock.Verify(
            mock =>
                mock.UpdateAsync(It.IsAny<Product>()),
            Times.Never);

        _notificatorMock.Verify(
            mock =>
                mock.Handle(It.IsAny<Notification>()),
            Times.Exactly(expectedNotifications.Length));
    }

    #endregion

    #region RemoveAsync

    [Fact(DisplayName = $"{ClassName} {nameof(ProductService.RemoveAsync)} should remove product")]
    public async Task UpdateAsync_ShouldRemoveProduct()
    {
        // Arrange
        var productId = Guid.NewGuid();

        _repositoryMock
            .Setup(mock =>
                mock.RemoveAsync(It.IsAny<Guid>()))
            .Callback((Guid idCb) =>
                idCb.Should().Be(productId));

        // Act
        await _service.RemoveAsync(productId);

        // Assert
        _repositoryMock.Verify(
            mock =>
                mock.RemoveAsync(It.IsAny<Guid>()),
            Times.Once);

        _notificatorMock.Verify(
            mock =>
                mock.Handle(It.IsAny<Notification>()),
            Times.Never);
    }

    #endregion

    #region Disponse

    [Fact(DisplayName = $"{ClassName} {nameof(ProductService.Dispose)} should dispose repository")]
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