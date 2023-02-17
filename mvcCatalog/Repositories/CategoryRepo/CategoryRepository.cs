using Microsoft.EntityFrameworkCore;
using mvcCatalog.Data;
using mvcCatalog.Models;
using mvcCatalog.Repositories.GenericRepository;

namespace mvcCatalog.Repositories.CategoryRepo;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(DataContext dataContext, IServiceProvider serviceProvider) : base(dataContext, serviceProvider)
    {
    }


    public async Task<IEnumerable<Category>> getWithNoParent()
    {
        var categories = await _dbSet
            .Where(c => c.ParentCategoryId == null)
            .Include(c => c.ChildrenCategories)
            .Include(c => c.ParentCategory)
            .OrderBy(c => c.CategoryName)
            .ToListAsync();
        return categories;
    }

    public async Task<IEnumerable<Category>> getWithParent()
    {
        var categories = await _dbSet.Where(c => c.ParentCategory != null).OrderBy(c => c.CategoryName)
            .ToListAsync();
        return categories;
    }

    public async Task<IEnumerable<Category>> GetByParentId
        (int categoryId) => await _dbSet.Where(c => c.ParentCategoryId == categoryId)
        .OrderBy(c => c.CategoryName)
        .Include(c => c.Products)
        .ToListAsync();

    public bool isNull() => _dbSet == null;

    public Category getByIdIncludingEverything(int id)
    {
        return _dbSet.Where(c => c.CategoryId == id)
            .Include(c => c.ChildrenCategories)
            .Include(c => c.ParentCategory)
            .First();
    }

    public new async Task<Category> Delete(int id)
    {
        var entity = getByIdIncludingEverything(id);
        if (entity != null)
            await DeleteEntity(entity);
        else
            Console.WriteLine("GenericRepository -> Delete: object is null!");


        return entity;
    }
}