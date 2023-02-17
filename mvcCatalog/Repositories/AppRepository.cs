using mvcCatalog.Repositories.CategoryRepo;
using mvcCatalog.Repositories.ProductFromSupplierRepo;
using mvcCatalog.Repositories.ProductRepo;
using mvcCatalog.Repositories.Services;

namespace mvcCatalog.Repositories;

public class AppRepository
{

    public AppRepository(IProductRepository productRepo, ICategoryRepository categoryRepo,
        IProductFromSupplierRepository productFromSupplierRepo, IBucketService bucketService)
    {
        BucketService = bucketService;
        ProductRepo = productRepo;
        CategoryRepo = categoryRepo;
        ProductFromSupplierRepo = productFromSupplierRepo;
    }

    public IBucketService BucketService { get; }
    public IProductRepository ProductRepo { get; }
    public ICategoryRepository CategoryRepo { get; }
    public IProductFromSupplierRepository ProductFromSupplierRepo { get; }
}