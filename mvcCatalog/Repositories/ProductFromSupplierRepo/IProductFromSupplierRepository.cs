namespace mvcCatalog.Repositories.ProductFromSupplierRepo;

public interface IProductFromSupplierRepository
{
    Tuple<decimal, decimal>? GetMinMaxPriceByProductId(int productId);

}