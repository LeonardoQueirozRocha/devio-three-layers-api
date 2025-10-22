using DevIO.Business.Entities;
using DevIO.Business.Interfaces.Repositories.Base;

namespace DevIO.Business.Interfaces.Repositories;

public interface ISupplierRepository : IRepository<Supplier>
{
    Task<Supplier> GetSupplierWithAddressAsync(Guid id);

    Task<Supplier> GetSupplierWithProductsAndAddressAsync(Guid id);

    Task<Address> GetAddressBySupplierIdAsync(Guid supplierId);
}