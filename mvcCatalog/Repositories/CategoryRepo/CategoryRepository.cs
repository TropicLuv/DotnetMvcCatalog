using Microsoft.EntityFrameworkCore;
using mvcCatalog.Data;
using mvcCatalog.Models;
using mvcCatalog.Repositories.GenericRepository;

namespace mvcCatalog.Repositories.CategoryRepo;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(DataContext dataContext) : base(dataContext.Categories)
    {
    }

    public async Task<IEnumerable<Category>> getWithNoParent()
    {
        var categories = await _dbSet
            .Where(c => c.ParentCategoryId == null)
            .OrderBy(c => c.CategoryName)
            .ToListAsync();
        return categories;
    }

    public IQueryable<Category> GetByParentId(int categoryId)
    {
        return _dbSet.Where(c => c.ParentCategoryId == categoryId);
    }

    public bool isNull()
    {
        return _dbSet == null;
    }
}