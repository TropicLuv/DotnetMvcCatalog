using mvcCatalog.Models;

namespace mvcCatalog.Repositories.Services;

public class BucketService : IBucketService
{
    public int ReadyToPayAmount { get; set; }
    public List<ProductFromSupplier> ProductsFromSuppliers { get; set; } 

    public int GetReadyToPayProductsAmount() => ReadyToPayAmount;
    public void AddProductFromSuppliers()
    {
        ReadyToPayAmount += 1;
        // ProductsFromSuppliers.Add(productFromSupplier);
    }


}