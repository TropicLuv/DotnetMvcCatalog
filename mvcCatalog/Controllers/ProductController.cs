using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvcCatalog.Models;
using mvcCatalog.Repositories;
using mvcCatalog.Repositories.Services;

namespace mvcCatalog.Controllers;

public class ProductController : Controller
{
    private readonly AppRepository _repo;
    private readonly IServiceProvider _serviceProvider;

    public ProductController(AppRepository repo, IServiceProvider serviceProvider)
    {
        _repo = repo;
        _serviceProvider = serviceProvider;
    }

    public IActionResult Create(string parentCategoryId, string categoryId)
    {
        ViewData["ParentCategoryId"] = parentCategoryId;
        ViewData["CategoryId"] = categoryId;


        return View();
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        if (ModelState.IsValid)
        {
            var category = await _repo.CategoryRepo.getById(product.CategoryId);
            product.Category = category;
            await _repo.ProductRepo.Insert(product);
            return RedirectToAction(nameof(Products), new { categoryId = product.CategoryId.ToString() });
        }

        var errors = ModelState.Select(x => x.Value.Errors)
            .Where(y => y.Count > 0)
            .ToList();

        ValidationProblem(ModelState);
        return View(product);
    }


    [HttpGet]
    public IActionResult Edit(string productId)
    {
        ViewData["productId"] = productId;
        return View(_repo.ProductRepo.getByIdIncludingEverything(int.Parse(productId)));
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Product product)
    {
        if (ModelState.IsValid)
        {
            _repo.ProductRepo.SetValues(product.ProductId, product);
            return RedirectToAction(nameof(Products), new { categoryId = product.CategoryId.ToString() });
        }

        ValidationProblem(ModelState);
        return View(product);
    }

    // [HttpGet]
    // public IActionResult Delete(string id) => View(id);

    [HttpPost]
    public async void DeleteProduct(string productId)
    {
        // await _repo.ProductRepo.Delete(int.Parse(productId));
        
        using (var scope = _serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetService<AppRepository>();
            var deletedProduct = await context.ProductRepo.Delete(int.Parse(productId));
        }
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
    public async Task<PartialViewResult> Products
    (string categoryId,
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


    public async Task<IActionResult> SingleProductView(string productId)
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

        return View(model);
    }
    
    [HttpPost]
    public IActionResult AddToBucket()
    {
        
        _repo.BucketService.AddProductFromSuppliers();

        return Ok();
    }
}