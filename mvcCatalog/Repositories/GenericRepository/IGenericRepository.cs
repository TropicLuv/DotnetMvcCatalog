namespace mvcCatalog.Repositories.GenericRepository;

public interface IGenericRepository<TEntity>
{
    Task<IEnumerable<TEntity>> getAll();

    Task<TEntity?> getById(int id);

    public Task Insert(TEntity obj);
    public Task SaveChanges();

    void Update(TEntity obj);
    Task<TEntity> SetValues(int id, TEntity entityChanged);
    Task<TEntity> Delete(int id);
    Task DeleteEntity(TEntity entity);
}