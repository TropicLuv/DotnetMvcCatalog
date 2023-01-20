using System.ComponentModel.DataAnnotations;

namespace mvcCatalog.Models;

public class Supplier
{
    public int SupplierId { get; set; }

    [StringLength(60)] [Required] public string SupplierName { get; set; }

    [Display(Name = "Registration Date")]
    [DataType(DataType.Date)]
    public DateTime RegistrationDate { get; set; } = new DateTime().Date;

    public ICollection<ProductFromSupplier>? ProductsFromSupplier { get; set; }
}