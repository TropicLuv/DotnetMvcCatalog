namespace mvcCatalog.Models;

public class SingleProductView
{
    public Tuple<Product, Tuple<decimal, decimal>> ProductWithPriceRange { get; set; }
    public Dictionary<Product, Tuple<decimal, decimal>>? SimilarProductsWithPriceRange { get; set; }
}