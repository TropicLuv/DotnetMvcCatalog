using mvcCatalog.Models;

namespace mvcCatalog.Repositories.ProductRepo;

public interface IProductRepository
{
    public Product? GetByIdIncludingSuppliers(int id);
    public IQueryable<Product> GetByCategoryId(int categoryId);
    public IQueryable<Product> isDiscountByCategoryId(int categoryId);
    public IQueryable<Product> GetEmptyQuery();
    public Task<IQueryable<Product>> GetSimilarByName(int productId);
}