namespace mvcCatalog.Repositories.GenericRepository;

public interface IGenericRepository<TEntity>
{
    Task<IEnumerable<TEntity>> getAll();

    Task<TEntity?> getById(int id);
}