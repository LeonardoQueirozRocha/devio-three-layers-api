using DevIO.Business.Entities.Base;

namespace DevIO.Business.Entities;

public class Product : Entity
{
    public string? Name { get; set; }
    
    public string? Description { get; set; }
    
    public decimal Value { get; set; }
    
    public DateTime RegistrationDate { get; set; }
    
    public bool Active { get; set; }
}