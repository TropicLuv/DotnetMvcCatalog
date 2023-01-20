using mvcCatalog.Repositories.CategoryRepo;
using mvcCatalog.Repositories.ProductFromSupplierRepo;
using mvcCatalog.Repositories.ProductRepo;

namespace mvcCatalog.Repositories;

public class AppRepository
{
    public AppRepository(ProductRepository productRepo, CategoryRepository categoryRepo,
        ProductFromSupplierRepository productFromSupplierRepo)
    {
        ProductRepo = productRepo;
        CategoryRepo = categoryRepo;
        ProductFromSupplierRepo = productFromSupplierRepo;
    }

    public ProductRepository ProductRepo { get; }
    public CategoryRepository CategoryRepo { get; }
    public ProductFromSupplierRepository ProductFromSupplierRepo { get; }
}