using mvcCatalog.Models;

namespace mvcCatalog.Repositories.ProductFromSupplierRepo;

public interface IProductFromSupplierRepository
{

    ProductFromSupplier GetByProductIdAndSupplierId(int productId, int supplierId);
    Tuple<decimal, decimal>? GetMinMaxPriceByProductId(int productId);
    IQueryable<Product> GetProductsByPriceRangeAndCategoryId(int categoryId, Tuple<int, int> priceRange);

    public Task Insert(ProductFromSupplier obj);
    public Task SaveChanges();
}