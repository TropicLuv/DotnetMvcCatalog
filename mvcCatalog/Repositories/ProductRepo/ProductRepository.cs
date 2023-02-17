using Microsoft.EntityFrameworkCore;
using mvcCatalog.Data;
using mvcCatalog.Models;
using mvcCatalog.Repositories.GenericRepository;

namespace mvcCatalog.Repositories.ProductRepo;

public class ProductRepository : GenericRepository<Product>, IProductRepository


{
    public ProductRepository(DataContext dataContext, IServiceProvider serviceProvider) : base(dataContext, serviceProvider)
    {
    }


    public async Task<Product> getByIdIncludingCategory(int id)
    {
        return await _dbSet.Where(p => p.ProductId == id)
            .Include(p => p.Category)
            .FirstAsync();
    }

    public Product? GetByIdIncludingSuppliers(int id)
    {
        return _dbSet.Where(p => p.ProductId == id)
            .Include(p => p.ProductFromSuppliers).ThenInclude(pfs => pfs.Supplier)
            .FirstOrDefault();
    }

    public Product getByIdIncludingEverything(int id) => _dbSet.Where(p => p.ProductId == id)
        .Include(p => p.Category).ThenInclude(c => c.ParentCategory)
        .Include(p => p.ProductFromSuppliers)
        .First();

    public IQueryable<Product> GetByCategoryId(int categoryId)
    {
        return _dbSet.Where(p => p.CategoryId == categoryId);
    }


    public IQueryable<Product> isDiscountByCategoryId(int categoryId)
    {
        return GetByCategoryId(categoryId).Where(p => p.ProductFromSuppliers.Any(pfs => pfs.IsDiscount == true));
    }

    public IQueryable<Product> GetEmptyQuery() => _dbSet.AsQueryable();

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