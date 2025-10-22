using DevIO.Business.Entities;
using DevIO.Business.Interfaces.Repositories;
using DevIO.Business.Interfaces.Services;
using DevIO.Business.Services.Base;

namespace DevIO.Business.Services;

public class ProductService(IProductRepository productRepository) : BaseService, IProductService
{
    public async Task AddAsync(Product product)
    {
        await productRepository.AddAsync(product);
    }

    public async Task UpdateAsync(Product product)
    {
        await productRepository.UpdateAsync(product);
    }

    public async Task RemoveAsync(Guid id)
    {
        await productRepository.RemoveAsync(id);
    }

    public void Dispose() =>
        productRepository?.Dispose();
}