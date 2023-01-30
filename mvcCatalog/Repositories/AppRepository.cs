using mvcCatalog.Repositories.CategoryRepo;
using mvcCatalog.Repositories.ProductFromSupplierRepo;
using mvcCatalog.Repositories.ProductRepo;

namespace mvcCatalog.Repositories;

public class AppRepository
{
    public AppRepository(IProductRepository productRepo, ICategoryRepository categoryRepo,
        IProductFromSupplierRepository productFromSupplierRepo)
    {
        ProductRepo = productRepo;
        CategoryRepo = categoryRepo;
        ProductFromSupplierRepo = productFromSupplierRepo;
    }

    public IProductRepository ProductRepo { get; }
    public ICategoryRepository CategoryRepo { get; }
    public IProductFromSupplierRepository ProductFromSupplierRepo { get; }
}