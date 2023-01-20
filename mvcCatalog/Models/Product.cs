using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvcCatalog.Models;

public class Product
{
    public int ProductId { get; set; }

    [StringLength(60)] [Required] public string ProductName { get; set; }

    [StringLength(160)] [Required] public string ProductDescription { get; set; }

    [StringLength(160)] public string? Image { get; set; }

    public IEnumerable<Image>? AlbumOfImages { get; set; }

    public int CategoryId { get; set; }

    [Required] public Category Category { get; set; }

    public ICollection<ProductFromSupplier> ProductFromSuppliers { get; set; }

    

    public required int Year { get; set; }

    [StringLength(160)] public string Features { get; set; }

    public int Weight { get; set; }

    public required string Manufacturer { get; set; }
}