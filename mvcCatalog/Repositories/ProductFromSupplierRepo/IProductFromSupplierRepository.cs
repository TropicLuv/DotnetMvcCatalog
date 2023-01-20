using mvcCatalog.Models;

namespace mvcCatalog.Repositories.ProductFromSupplierRepo;

public interface IProductFromSupplierRepository
{
    Tuple<decimal, decimal>? GetMinMaxPriceByProductId(int productId);
    IQueryable<Product> GetProductsByPriceRangeAndCategoryId(int categoryId, Tuple<int, int> priceRange);
}