using DevIO.Business.Entities;
using DevIO.Business.Entities.Validations;
using DevIO.Business.Interfaces.Repositories;
using DevIO.Business.Interfaces.Services;
using DevIO.Business.Services.Base;

namespace DevIO.Business.Services;

public class ProductService(IProductRepository productRepository) : BaseService, IProductService
{
    public async Task AddAsync(Product product)
    {
        if (!IsProductValid(product))
        {
            return;
        }

        await productRepository.AddAsync(product);
    }

    public async Task UpdateAsync(Product product)
    {
        if (!IsProductValid(product))
        {
            return;
        }

        await productRepository.UpdateAsync(product);
    }

    public async Task RemoveAsync(Guid id)
    {
        await productRepository.RemoveAsync(id);
    }

    public void Dispose() =>
        productRepository?.Dispose();

    #region Private Methods

    private bool IsProductValid(Product product) =>
        Validate(new ProductValidation(), product);

    #endregion
}