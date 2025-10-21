using DevIO.Business.Entities;

namespace DevIO.Business.Interfaces;

public interface ISupplierService : IDisposable
{
    Task AddAsync(Supplier supplier);
    
    Task UpdateAsync(Supplier supplier);
    
    Task RemoveAsync(Guid id);
}