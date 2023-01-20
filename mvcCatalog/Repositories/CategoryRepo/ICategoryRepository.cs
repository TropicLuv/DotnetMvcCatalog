using mvcCatalog.Models;

namespace mvcCatalog.Repositories.CategoryRepo;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> getWithNoParent();

    IQueryable<Category> GetByParentId(int categoryId);

    bool isNull();
}