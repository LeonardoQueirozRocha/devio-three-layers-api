using System.ComponentModel.DataAnnotations;
using DevIO.Business.Enums;

namespace DevIO.Api.ViewModels;

public class SupplierViewModel
{
    [Key] 
    public Guid Id { get; set; }

    [Required(ErrorMessage = "The field {0} is required")]
    [StringLength(100, ErrorMessage = "The field {0} should be between {2} and {1} characters", MinimumLength = 2)]
    public string? Name { get; set; }

    [Required(ErrorMessage = "The field {0} is required")]
    [StringLength(14, ErrorMessage = "The field {0} should be between {2} and {1} characters", MinimumLength = 2)]
    public string? Document { get; set; }

    public SupplierType SupplierType { get; set; }

    public bool Active { get; set; }

    public AddressViewModel? Address { get; set; }

    public IEnumerable<ProductViewModel>? Products { get; set; }
}