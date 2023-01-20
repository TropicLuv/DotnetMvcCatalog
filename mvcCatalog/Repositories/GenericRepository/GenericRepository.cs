using Microsoft.EntityFrameworkCore;

namespace mvcCatalog.Repositories.GenericRepository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly DbSet<T> _dbSet;

    protected GenericRepository(DbSet<T> dbSet)
    {
        _dbSet = dbSet;
    }


    public async Task<IEnumerable<T>> getAll()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T?> getById(int id)
    {
        return await _dbSet.FindAsync(id);
    }
}