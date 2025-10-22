using DevIO.Business.Entities;
using DevIO.Business.Entities.Validations;
using DevIO.Business.Interfaces.Repositories;
using DevIO.Business.Interfaces.Services;
using DevIO.Business.Services.Base;

namespace DevIO.Business.Services;

public class SupplierService(ISupplierRepository supplierRepository) : BaseService, ISupplierService
{
    public async Task AddAsync(Supplier supplier)
    {
        if (!IsSupplierAndAddressValid(supplier))
        {
            return;
        }

        await supplierRepository.AddAsync(supplier);
    }

    public async Task UpdateAsync(Supplier supplier)
    {
        if (!IsSupplierValid(supplier))
        {
            return;
        }

        await supplierRepository.UpdateAsync(supplier);
    }

    public async Task RemoveAsync(Guid id)
    {
        await supplierRepository.RemoveAsync(id);
    }

    public void Dispose() =>
        supplierRepository?.Dispose();

    #region Private Methods

    private bool IsSupplierValid(Supplier supplier) =>
        Validate(new SupplierValidation(), supplier);

    private bool IsAddressValid(Address? address) =>
        Validate(new AddressValidation(), address!);

    private bool IsSupplierAndAddressValid(Supplier supplier) =>
        IsSupplierValid(supplier) && IsAddressValid(supplier.Address);

    #endregion
}