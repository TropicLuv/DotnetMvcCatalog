using mvcCatalog.Models;

namespace mvcCatalog.Repositories.Services;

public interface IBucketService
{
    public int ReadyToPayAmount { get; set; }
    public List<ProductFromSupplier> ProductsFromSuppliers { get; set; }

    int GetReadyToPayProductsAmount();

    void AddProductFromSuppliers();
}