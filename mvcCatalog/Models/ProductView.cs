namespace mvcCatalog.Models;

public class ProductView
{
    public Dictionary<Product, Tuple<decimal, decimal>?> ProductsAndPrices { get; set; }
    public string CategoryId { get; set; }
    public IEnumerable<string> Manufacturers { get; set; }
    public IEnumerable<string> Years { get; set; }
}