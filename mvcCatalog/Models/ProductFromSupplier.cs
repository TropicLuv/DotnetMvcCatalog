using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvcCatalog.Models;

/*[PrimaryKey(nameof(ProductId), nameof(SupplierId))]*/
public class ProductFromSupplier
{
    public int ProductId { get; set; }

    public int SupplierId { get; set; }
    [Required] public Product Product { get; set; }
    [Required] public Supplier Supplier { get; set; }

    [Range(1, 100)]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18,2)")]
    public decimal? Price { get; set; }

    public required bool IsDiscount { get; set; }

    [Column(TypeName = "decimal(18,2)")] public decimal NewPrice { get; set; }
}