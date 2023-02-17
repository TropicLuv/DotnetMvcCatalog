using Microsoft.EntityFrameworkCore;
using mvcCatalog.Data;


namespace mvcCatalog.Repositories.GenericRepository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly DataContext _context;
    private readonly IServiceProvider _serviceProvider;
    protected readonly DbSet<T> _dbSet;

    protected GenericRepository(DataContext context, IServiceProvider serviceProvider)
    {
        _context = context;
        _serviceProvider = serviceProvider;
        _dbSet = context.Set<T>();
    }


    public async Task<IEnumerable<T>> getAll() => await _dbSet.ToListAsync();

    public async Task<T?> getById(int id) => await _dbSet.FindAsync(id);

    public async Task Insert(T obj)
    {
        _dbSet.Add(obj);
        await _context.SaveChangesAsync();
    }

    public async Task SaveChanges()
    {
        var a = await _context.SaveChangesAsync();
    }

    public void Update(T obj)
    {
        _dbSet.Update(obj);
    }

    public async Task<T> SetValues(int id, T entityChanged)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetService<DataContext>();
            context.Update(entityChanged);
            var a = await context.SaveChangesAsync();
        }


        return entityChanged;
    }

    public async Task DeleteEntity(T entity)
    {
        _context.Remove(entity);
        await SaveChanges();
    }

    public async Task<T> Delete(int id)
    {
        var entity = _context.Find<T>(id);
        if (entity != null)
            _context.Remove(entity);
        else
            Console.WriteLine("GenericRepository -> Delete: object is null!");

        await _context.SaveChangesAsync();

        return entity;
    }
}