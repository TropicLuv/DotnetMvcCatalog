using mvcCatalog.Data;
using mvcCatalog.Models;
using mvcCatalog.Repositories.GenericRepository;

namespace mvcCatalog.Repositories.ProductRepo;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(DataContext context) : base(context.Products)
    {
    }

    public IQueryable<Product> GetByCategoryId(int categoryId)
    {
        return _dbSet.Where(p => p.CategoryId == categoryId);
    }


    public IQueryable<Product> isDiscountByCategoryId(int categoryId)
    {
        return GetByCategoryId(categoryId).Where(p => p.ProductFromSuppliers.Any(pfs => pfs.IsDiscount == true));
    }

    public IQueryable<Product> GetEmptyQuery()
    {
        return _dbSet.AsQueryable();
    }

    public async Task<IQueryable<Product>> GetSimilarByName(int productId)
    {
        var product = await getById(productId);
        var nameToFindSimilarProducts = product?.ProductName.Split(" ")[0];
        var products = from p in _dbSet
            where p.ProductName.Contains(nameToFindSimilarProducts) && p.CategoryId == product.CategoryId
            select p;
        // _dbSet.Where(p => p.ProductName.Contains(nameToFindSimilarProducts));
        return products;
    }
}