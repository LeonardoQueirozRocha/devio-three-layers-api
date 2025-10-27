using DevIO.Business.Entities.Base;

namespace DevIO.Business.Entities;

public class Address : Entity
{
    public string? Street { get; set; }

    public string? Number { get; set; }

    public string? Complement { get; set; }

    public string? ZipCode { get; set; }

    public string? Neighborhood { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public Supplier? Supplier { get; set; }
}