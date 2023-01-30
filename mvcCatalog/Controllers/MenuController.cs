using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvcCatalog.Models;
using mvcCatalog.Repositories;

namespace mvcCatalog.Controllers;

public class MenuController : Controller
{
    private readonly AppRepository _repo;

    public MenuController(AppRepository repo)
    {
        _repo = repo;
    }

    public async Task<IActionResult> Categories()
    {
        if (_repo.CategoryRepo.isNull()) return Problem("Entity was not set");
        var categories = await _repo.CategoryRepo.getWithNoParent();
        return View("Categories", categories);
    }


    public async Task<PartialViewResult> Products(string categoryId)
    {
        var products = await _repo.ProductRepo.GetByCategoryId(int.Parse(categoryId)).ToListAsync();
        var setOfYears = new HashSet<string>();
        var manufacturers = new HashSet<string>();
        var minMaxProductPrices = new Dictionary<Product, Tuple<decimal, decimal>?>();

        foreach (var product in products)
        {
            setOfYears.Add(product.Year.ToString());
            manufacturers.Add(product.Manufacturer);
            var productMinMaxPrices = _repo.ProductFromSupplierRepo.GetMinMaxPriceByProductId(product.ProductId);
            minMaxProductPrices.Add(product, productMinMaxPrices);
        }

        var productView = new ProductView
        {
            ProductsAndPrices = minMaxProductPrices,
            CategoryId = categoryId,
            Manufacturers = manufacturers,
            Years = setOfYears
        };

        return PartialView("_ProductsView", productView);
    }


    [HttpPost]
    public async Task<PartialViewResult> Products(string categoryId,
        [Bind("Manufacturers", "IsDiscount", "PriceFrom, PriceTo")]
        ProductsFilter productsFilter)
    {
        IQueryable<Product> products;
        var PriceRange = new Tuple<int, int>(productsFilter.PriceFrom, productsFilter.PriceTo);

        if (PriceRange.Item1 == 0 && PriceRange.Item2 == 0)
            products = _repo.ProductRepo.GetByCategoryId(int.Parse(categoryId));
        else
            products =
                _repo.ProductFromSupplierRepo.GetProductsByPriceRangeAndCategoryId(int.Parse(categoryId), PriceRange);

        if (productsFilter.IsDiscount)
            products = _repo.ProductRepo.isDiscountByCategoryId(int.Parse(categoryId));

//TODO - ask koghu/mamuka about  if i use object to find something, using ef 
        if (productsFilter.Manufacturers != null)
            products = products.Where(p => productsFilter.Manufacturers
                .Contains(p.Manufacturer));

        var productsFiltered = await products.ToListAsync();

        var minMaxProductPrices = new Dictionary<Product, Tuple<decimal, decimal>?>();

        //Dict of (product, null) or (product, Tuple of (minMaxPrice))
        foreach (var product in productsFiltered)
        {
            //TODO - Optimize it later using Select
            var productMinMaxPrices =
                _repo.ProductFromSupplierRepo.GetMinMaxPriceByProductId(product.ProductId);

            minMaxProductPrices.Add(product, productMinMaxPrices);
        }


        return PartialView("_Products", minMaxProductPrices);
    }

    public async Task<PartialViewResult> PartialCategories(string categoryId)
    {
        var childrenCategories = await _repo.CategoryRepo.GetByParentId(int.Parse(categoryId)).ToListAsync();
        return PartialView("_ChildrenCategories", childrenCategories);
    }

    [SuppressMessage("ReSharper.DPA", "DPA0006: Large number of DB commands", MessageId = "count: 972")]
    public async Task<IActionResult> SingleProductView(string productId, string categoryId)
    {
        var product = _repo.ProductRepo.GetByIdIncludingSuppliers(int.Parse(productId));

        var similarProductsQuery = await _repo.ProductRepo.GetSimilarByName(int.Parse(productId));

        var products = await similarProductsQuery.ToListAsync();

        var similarProducts = new Dictionary<Product, Tuple<decimal, decimal>>();
        foreach (var similarProduct in products)
        {
            var similarProductPriceRange =
                _repo.ProductFromSupplierRepo.GetMinMaxPriceByProductId(similarProduct.ProductId);
            if (similarProductPriceRange != null && similarProduct.ProductId != int.Parse(productId))
                similarProducts.Add(
                    similarProduct,
                    similarProductPriceRange
                );
        }

        var model = new SingleProductView
        {
            ProductWithPriceRange = new Tuple<Product, Tuple<decimal, decimal>>(
                product, _repo.ProductFromSupplierRepo.GetMinMaxPriceByProductId(product.ProductId)
            ),
            SimilarProductsWithPriceRange = similarProducts
        };
        Console.WriteLine("HHHHHHHHHHHHH");
        foreach (var supplier in product.ProductFromSuppliers) Console.WriteLine(supplier.Supplier.SupplierName);
        Console.WriteLine("HHHHHHHHHHHHH");
        return View(model);
    }
}