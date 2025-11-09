using DevIO.Data.Repositories;
using DevIO.Data.Tests.Fixtures;
using DevIO.Data.Tests.Fixtures.Collections;
using DevIO.Util.Tests.Builders.Business.Entities;
using FluentAssertions;

namespace DevIO.Data.Tests.Repositories;

[Collection(nameof(DatabaseCollection))]
public class ProductRepositoryTests
{
    private const string ClassName = nameof(ProductRepository);

    private readonly DatabaseFixture _fixture;
    private readonly ProductRepository _repository;

    public ProductRepositoryTests(DatabaseFixture fixture)
    {
        _fixture = fixture;
        _repository = new ProductRepository(_fixture.Context);
    }

    #region GetAllAsync

    [Fact(DisplayName = $"{ClassName} {nameof(ProductRepository.GetAllAsync)} should return all products")]
    public async Task GetAllAsync_ShouldReturnAllProducts()
    {
        // Arrange
        var products = ProductBuilder.Instance.BuildCollection(3);

        await _fixture.SeedAsync(products);

        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        result.Should().NotBeNull().And.BeEquivalentTo(products);
    }

    #endregion

    #region GetByIdAsync

    [Fact(DisplayName = $"{ClassName} {nameof(ProductRepository.GetByIdAsync)} should return one product")]
    public async Task GetAllAsync_ShouldReturnOneProduct()
    {
        // Arrange
        var product = ProductBuilder.Instance.Build();

        await _fixture.SeedAsync([product]);

        // Act
        var result = await _repository.GetByIdAsync(product.Id);

        // Assert
        result.Should().NotBeNull().And.BeEquivalentTo(product);
    }

    #endregion

    #region FindAsync

    [Fact(DisplayName = $"{ClassName} {nameof(ProductRepository.FindAsync)} should return products using a predicate")]
    public async Task GetAllAsync_ShouldReturnProductsUsingPredicate()
    {
        // Arrange
        var product = ProductBuilder.Instance.Build();
        product.Name = "Test";
        
        await _fixture.SeedAsync([product]);

        // Act
        var result = await _repository.FindAsync(p => p.Name == "Test");

        // Assert
        result.Should().NotBeNull().And.BeEquivalentTo([product]);   
    }

    #endregion

    #region AddAsync

    [Fact(DisplayName = $"{ClassName} {nameof(ProductRepository.AddAsync)} should add product")]
    public async Task AddAsync_ShouldAddProduct()
    {
        // Arrange
        var product = ProductBuilder.Instance.Build();
        
        // Act
        await _repository.AddAsync(product);
        
        // Assert
        var result = await _repository.GetByIdAsync(product.Id);
        result.Should().Be(product);
    }

    #endregion
    
    #region UpdateAsync

    [Fact(DisplayName = $"{ClassName} {nameof(ProductRepository.UpdateAsync)} should update product")]
    public async Task UpdateAsync_ShouldAddProduct()
    {
        // Arrange
        var product = ProductBuilder.Instance.Build();
        await _repository.AddAsync(product);
        
        var updatedProduct = await _repository.GetByIdAsync(product.Id);
        updatedProduct!.Name = "Updated";
        
        // Act
        await _repository.UpdateAsync(updatedProduct);
        
        // Assert
        var result = await _repository.GetByIdAsync(product.Id);
        result.Should().Be(product);
    }

    #endregion
    
    #region RemoveAsync

    [Fact(DisplayName = $"{ClassName} {nameof(ProductRepository.RemoveAsync)} should remove product")]
    public async Task RemoveAsync_ShouldRemoveProduct()
    {
        // Arrange
        var product = ProductBuilder.Instance.Build();
        
        await _fixture.SeedAsync([product]);
        
        // Act
        await _repository.RemoveAsync(product.Id);
        
        // Assert
        var result = await _repository.GetByIdAsync(product.Id);
        result.Should().BeNull();
    }

    #endregion
}