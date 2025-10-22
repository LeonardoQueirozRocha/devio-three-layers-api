using DevIO.Business.Entities.Base;
using DevIO.Business.Enums;

namespace DevIO.Business.Entities;

public class Supplier : Entity
{
    public string? Name { get; set; }

    public string? Document { get; set; }

    public SupplierType SupplierType { get; set; }

    public bool Active { get; set; }

    public Address? Address { get; set; }

    public IEnumerable<Product>? Products { get; set; }

    public bool IsNaturalPerson => SupplierType is SupplierType.NaturalPerson;

    public bool IsLegalEntity => SupplierType is SupplierType.LegalEntity;
}