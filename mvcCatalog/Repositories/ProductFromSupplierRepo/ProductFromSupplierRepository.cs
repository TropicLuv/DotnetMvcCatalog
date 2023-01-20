using Microsoft.IdentityModel.Tokens;
using mvcCatalog.Data;
using mvcCatalog.Models;
using mvcCatalog.Repositories.GenericRepository;

namespace mvcCatalog.Repositories.ProductFromSupplierRepo;

public class ProductFromSupplierRepository : GenericRepository<ProductFromSupplier>, IProductFromSupplierRepository
{
    public ProductFromSupplierRepository(DataContext dataContext) : base(dataContext.ProductsFromSuppliers)
    {
    }

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
    
}