using DevIO.Business.Entities;
using DevIO.Business.Interfaces.Repositories.Base;

namespace DevIO.Business.Interfaces.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetProductsBySupplierIdAsync(Guid supplierId);

    Task<IEnumerable<Product>> GetProductsWithSuppliersAsync();

    Task<IEnumerable<Product>> GetProductWithSupplierAsync(Guid id);
}