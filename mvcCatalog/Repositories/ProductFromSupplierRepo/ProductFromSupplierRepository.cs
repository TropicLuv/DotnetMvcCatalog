using Microsoft.EntityFrameworkCore;
using mvcCatalog.Data;
using mvcCatalog.Models;
using mvcCatalog.Repositories.GenericRepository;

namespace mvcCatalog.Repositories.ProductFromSupplierRepo;

public class ProductFromSupplierRepository : GenericRepository<ProductFromSupplier>, IProductFromSupplierRepository
{
    public ProductFromSupplierRepository
        (DataContext dataContext, IServiceProvider serviceProvider) : base(dataContext, serviceProvider)
    {
    }


    public ProductFromSupplier GetByProductIdAndSupplierId(int productId, int supplierId) => _dbSet
                                             .Where(pfs => pfs.ProductId == productId)
                                             .Include(pfs => pfs.Product)
                                             .Include(pfs => pfs.Supplier)
                                             .First(pfs => pfs.SupplierId == supplierId);

    public Tuple<decimal, decimal>? GetMinMaxPriceByProductId(int productId)
    {
        var minMaxPrices = _dbSet
            .Where(pfs => pfs.ProductId == productId)
            .GroupBy(pfs => 1)
            .Select(g => new
            {
                MinPrice = g.Min(pfs => pfs.Price),
                MaxPrice = g.Max(pfs => pfs.Price)
            })
            .FirstOrDefault();
        if (minMaxPrices != null)
            return new Tuple<decimal, decimal>((decimal)minMaxPrices.MinPrice!,
                (decimal)minMaxPrices.MaxPrice!);

        return null;
    }

    public IQueryable<Product> GetProductsByPriceRangeAndCategoryId(int categoryId, Tuple<int, int> priceRange)
    {
        return _dbSet
            .Where(pfs => pfs.Product.CategoryId == categoryId)
            .Where(pfs => pfs.Price >= priceRange.Item1 && pfs.Price <= priceRange.Item2)
            .Select(pfs => pfs.Product).Distinct();
    }
}