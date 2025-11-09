using DevIO.Business.Entities;
using DevIO.Business.Interfaces.Repositories;
using DevIO.Data.Context;
using DevIO.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Data.Repositories;

public class SupplierRepository(BusinessDbContext dbContext) : Repository<Supplier>(dbContext), ISupplierRepository
{
    public async Task<Supplier?> GetSupplierWithAddressAsync(Guid id) =>
        await DbSet
            .AsNoTracking()
            .Include(supplier => supplier.Address)
            .FirstOrDefaultAsync(supplier => supplier.Id == id);

    public async Task<Supplier?> GetSupplierWithProductsAndAddressAsync(Guid id) =>
        await DbSet
            .AsNoTracking()
            .Include(supplier => supplier.Products)
            .Include(supplier => supplier.Address)
            .FirstOrDefaultAsync(supplier => supplier.Id == id);

    public async Task<Address?> GetAddressBySupplierIdAsync(Guid supplierId) =>
        await DbContext.Addresses
            .AsNoTracking()
            .FirstOrDefaultAsync(address => address.SupplierId == supplierId);

    public async Task RemoveAddressAndSupplierAsync(Address address)
    {
        DbContext.Addresses.Remove(address);
        await SaveChangesAsync();
    }
}