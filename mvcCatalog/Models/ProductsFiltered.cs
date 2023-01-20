namespace mvcCatalog.Models;

public class ProductsFiltered
{
    public Dictionary<Product, Tuple<decimal, decimal>?> MinMaxProductPriceDictionary { get; set; }
}