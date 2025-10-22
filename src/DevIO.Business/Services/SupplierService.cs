using DevIO.Business.Entities;
using DevIO.Business.Interfaces.Repositories;
using DevIO.Business.Interfaces.Services;
using DevIO.Business.Services.Base;

namespace DevIO.Business.Services;

public class SupplierService(ISupplierRepository supplierRepository) : BaseService, ISupplierService
{
    public async Task AddAsync(Supplier supplier)
    {
        await supplierRepository.AddAsync(supplier);
    }

    public async Task UpdateAsync(Supplier supplier)
    {
        await supplierRepository.UpdateAsync(supplier);
    }

    public async Task RemoveAsync(Guid id)
    {
        await supplierRepository.RemoveAsync(id);
    }

    public void Dispose() =>
        supplierRepository?.Dispose();
}