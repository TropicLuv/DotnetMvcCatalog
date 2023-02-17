using mvcCatalog.Models;

namespace mvcCatalog.Repositories.ProductRepo;

public interface IProductRepository
{
    Task<Product> getByIdIncludingCategory(int id);

    public Product? GetByIdIncludingSuppliers(int id);
    Product getByIdIncludingEverything(int id);

    public IQueryable<Product> GetByCategoryId(int categoryId);
    public IQueryable<Product> isDiscountByCategoryId(int categoryId);
    public IQueryable<Product> GetEmptyQuery();
    public Task<IQueryable<Product>> GetSimilarByName(int productId);

    public Task Insert(Product obj);
    public Task SaveChanges();
    void Update(Product obj);
    Task<Product> SetValues(int id, Product entityChanged);
    Task<Product> Delete(int id);
}