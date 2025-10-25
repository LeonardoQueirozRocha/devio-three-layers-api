using DevIO.Business.Constants;
using DevIO.Business.Entities;
using DevIO.Business.Entities.Validations;
using DevIO.Business.Interfaces;
using DevIO.Business.Interfaces.Repositories;
using DevIO.Business.Interfaces.Services;
using DevIO.Business.Services.Base;

namespace DevIO.Business.Services;

public class SupplierService(
    ISupplierRepository repository,
    INotificator notificator)
    : BaseService(notificator), ISupplierService
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

        await repository.AddAsync(supplier);
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

        await repository.UpdateAsync(supplier);
    }

    public async Task RemoveAsync(Guid id)
    {
        var supplier = await repository.GetSupplierWithProductsAndAddressAsync(id);

        if (supplier is null)
        {
            Notify(SupplierValidationMessages.SupplierDoesNotExists);
            return;
        }

        if (supplier.HasProducts)
        {
            Notify(SupplierValidationMessages.SupplierHasProducts);
            return;
        }

        if (supplier.Address is not null)
        {
            await repository.RemoveAddressAndSupplierAsync(supplier.Address);
        }

        await repository.RemoveAsync(id);
    }

    public void Dispose() =>
        repository?.Dispose();

    #region Private Methods

    private bool IsSupplierValid(Supplier supplier) =>
        Validate(new SupplierValidation(), supplier);

    private bool IsAddressValid(Address? address) =>
        Validate(new AddressValidation(), address!);

    private bool IsSupplierAndAddressValid(Supplier supplier) =>
        IsSupplierValid(supplier) || IsAddressValid(supplier.Address);

    private async Task<bool> SupplierAlreadyExistsAsync(string? document)
    {
        var suppliers = await repository.FindAsync(supplier => supplier.Document == document);

        return suppliers.Any();
    }

    private async Task<bool> SupplierAlreadyExistsAsync(string? document, Guid id)
    {
        var suppliers = await repository.FindAsync(s => s.Document == document && s.Id != id);

        return suppliers.Any();
    }

    #endregion
}