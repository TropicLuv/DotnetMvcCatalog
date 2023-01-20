namespace mvcCatalog.Models;

public class ProductsFilter
{
    public List<string>? Manufacturers { get; set; }
    public bool IsDiscount { get; set; }
    public int PriceFrom { get; set; }
    public int PriceTo { get; set; }

    // public string toString()
    // {
    //     return PriceRange.Item1 + PriceRange.Item2.ToString();
    // }
}