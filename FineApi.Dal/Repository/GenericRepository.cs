using System.Linq.Expressions;
using FineApi.Service.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace FineApi.Dal.Repository;
public class GenericRepository<T>:IGenericRepository<T> where T : class
{
    private readonly DbSet<T> _set;
    private readonly DbContext _context;
    public GenericRepository(FineDbContext context)
    {
        _context = context;
        _set = _context.Set<T>();
    }
    public IQueryable<T?> Set => _set;
    
    public T? Find(object? id) => _set.Find(id);
    public ValueTask<T> FindAsync(object id, CancellationToken cancellationToken = default) => _set.FindAsync(id, cancellationToken)!;
    public T First(Expression<Func<T, bool>> expression) => _set.First(expression!)!;
    public Task<T> FirstAsync(Expression<Func<T, bool>>? expression, CancellationToken cancellationToken = default)
    {
        if (expression == default)
        {
            throw new Exception();
        }

        return _set.FirstAsync(expression, cancellationToken);
    }
    public T? FirstOrDefault(Expression<Func<T, bool>>? expression)
    {
        return expression is null ? _set.FirstOrDefault() : _set.FirstOrDefault(expression);
    }
    public Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>>? expression, CancellationToken cancellationToken = default)
    {
        return expression is not null ? _set.FirstOrDefaultAsync(expression, cancellationToken) : _set.FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
    
    public T Single(Expression<Func<T, bool>>? expression)
    {
        return expression is null ? _set.Single() : _set.Single(expression);
    }
    public Task<T> SingleAsync(Expression<Func<T, bool>>? expression, CancellationToken cancellationToken = default)
    {
        return expression is null ? _set.SingleAsync(cancellationToken: cancellationToken) : _set.SingleAsync(expression, cancellationToken);
    }

    public Task<IQueryable<T>> GetAll()
    {
        return Task.FromResult(_set.AsNoTracking());
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await _set.FindAsync(id);
    }
    public async ValueTask AddAsync(T entity) =>await _set.AddAsync(entity);
    public async ValueTask AddRangeAsync(List<T> entity)
    {
        await _set.AddRangeAsync(entity);
    }

    public ValueTask UpdateAsync(T entity)
    {
            _set.Attach(entity);
            var item = _context.Entry(entity);
            item.State = EntityState.Modified;
            item.CurrentValues.SetValues(entity);
            return ValueTask.CompletedTask;
    }
    public bool StateChanged()
    {
        var result=_context.ChangeTracker.HasChanges();
        
        return result ? true : false;
    }
    async Task<bool> IGenericRepository<T>.AnyAsync(Expression<Func<T, bool>> predicate)
    {
        return await _set.AnyAsync(predicate);
    }
}
