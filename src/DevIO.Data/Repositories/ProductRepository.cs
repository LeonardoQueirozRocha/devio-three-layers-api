using DevIO.Business.Entities;
using DevIO.Business.Interfaces.Repositories;
using DevIO.Data.Context;
using DevIO.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Data.Repositories;

public class ProductRepository(BusinessDbContext dbContext) : Repository<Product>(dbContext), IProductRepository
{
    public async Task<Product?> GetProductWithSupplierAsync(Guid id) =>
        await DbSet
            .AsNoTracking()
            .Include(product => product.Supplier)
            .FirstOrDefaultAsync(product => product.Id == id);

    public async Task<IEnumerable<Product>> GetProductsBySupplierIdAsync(Guid supplierId) => 
        await FindAsync(product => product.SupplierId == supplierId);

    public async Task<IEnumerable<Product>> GetProductsWithSuppliersAsync() =>
        await DbSet
            .AsNoTracking()
            .Include(product => product.Supplier)
            .OrderBy(product => product.Name)
            .ToListAsync();
}