using mvcCatalog.Models;

namespace mvcCatalog.Repositories.ProductRepo;

public interface IProductRepository
{
    public IQueryable<Product> GetByCategoryId(int categoryId);
    public IQueryable<Product> isDiscountByCategoryId(int categoryId);
    public IQueryable<Product> GetEmptyQuery();
}