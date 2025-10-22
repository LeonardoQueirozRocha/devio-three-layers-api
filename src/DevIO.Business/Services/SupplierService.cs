using DevIO.Business.Constants;
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

        if (await SupplierAlreadyExistsAsync(supplier.Document))
        {
            Notify(SupplierValidationMessages.SupplierAlreadyExists);
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

        if (await SupplierAlreadyExistsAsync(supplier.Document, supplier.Id))
        {
            Notify(SupplierValidationMessages.SupplierAlreadyExists);
            return;
        }

        await supplierRepository.UpdateAsync(supplier);
    }

    public async Task RemoveAsync(Guid id)
    {
        var supplier = await supplierRepository.GetSupplierWithProductsAndAddressAsync(id);

        if (supplier is null)
        {
            Notify(SupplierValidationMessages.SupplierDotNotExists);
            return;
        }

        if (supplier.Products!.Any())
        {
            Notify(SupplierValidationMessages.SupplierHasProducts);
            return;
        }

        if (supplier.Address is not null)
        {
            await supplierRepository.RemoveAddressAndSupplierAsync(supplier.Address);
        }

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

    private async Task<bool> SupplierAlreadyExistsAsync(string? document)
    {
        var suppliers = await supplierRepository.FindAsync(s => s.Document == document);

        return suppliers.Any();
    }

    private async Task<bool> SupplierAlreadyExistsAsync(string? document, Guid id)
    {
        var suppliers = await supplierRepository.FindAsync(s => s.Document == document && s.Id != id);

        return suppliers.Any();
    }

    #endregion
}