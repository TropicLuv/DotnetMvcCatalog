using Microsoft.AspNetCore.Mvc;
using mvcCatalog.Models;
using mvcCatalog.Repositories;

namespace mvcCatalog.Controllers;

public class CategoryController : Controller
{
    private readonly AppRepository _repo;
    private readonly IServiceProvider _serviceProvider;

    public CategoryController(AppRepository repo, IServiceProvider serviceProvider)
    {
        _repo = repo;
        _serviceProvider = serviceProvider;
    }


    public IActionResult Create(string categoryId)
    {
        ViewData["parentCategoryId"] = categoryId;
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Category category)
    {
        if (ModelState.IsValid)
        {
            await _repo.CategoryRepo.Insert(category);
            return RedirectToAction(nameof(Categories),  new { parentCategoryId = category.ParentCategoryId.ToString() });
        }

        ValidationProblem(ModelState);
        return View(category);
    }


    [HttpGet]
    public IActionResult Edit
        (string categoryId) => View(_repo.CategoryRepo.getByIdIncludingEverything(int.Parse(categoryId)));

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit
        ([Bind("CategoryId, CategoryName, Image, Description, ParentCategoryId")] Category category)
    {
        if (ModelState.IsValid)
        {
            _repo.CategoryRepo.SetValues(category.CategoryId, category);
            return RedirectToAction(nameof(Categories),
                new { parentCategoryId = category.ParentCategoryId.ToString() });
        }

        ValidationProblem(ModelState);
        return View(category);
    }


    [HttpGet]
    public IActionResult Delete(string id) => View(id);

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteCategory(string id)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetService<AppRepository>();
            var deleted_category = await context.CategoryRepo.Delete(int.Parse(id));

            // _repo.CategoryRepo.Delete(int.Parse(id));
            return RedirectToAction(nameof(Categories),
                new { parentCategoryId = deleted_category.ParentCategoryId.ToString() });
        }
    }


    public async Task<IActionResult> Categories(string parentCategoryId)
    {
        ViewData["parentCategoryForChildrenRenderId"] = parentCategoryId;
        if (_repo.CategoryRepo.isNull()) return Problem("Entity was not set");
        // var categories = await _repo.CategoryRepo.getWithNoParent();
        return View("Categories");
    }

    public async Task<PartialViewResult> PartialCategories(string categoryId)
    {
        ViewData["parentCategoryId"] = categoryId;
        var childrenCategories = await _repo.CategoryRepo.GetByParentId(int.Parse(categoryId));
        return PartialView("_ChildrenCategories", childrenCategories);
    }
}