using mvcCatalog.Models;

namespace mvcCatalog.Repositories.CategoryRepo;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> getWithNoParent();

    Task<IEnumerable<Category>> getWithParent();


    Task<IEnumerable<Category>> GetByParentId(int categoryId);

    bool isNull();
    public Task Insert(Category obj);
    public Task SaveChanges();
    Task<Category> getById(int id);
    void Update(Category obj);
    Category getByIdIncludingEverything(int id);
    Task<Category> SetValues(int id, Category entityChanged);
    Task<Category> Delete(int id);
    Task DeleteEntity(Category entity);
}