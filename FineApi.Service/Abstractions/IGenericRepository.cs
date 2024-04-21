using System.Linq.Expressions;

namespace FineApi.Service.Abstractions;
public interface IGenericRepository<T> where T : class
{
    IQueryable<T?> Set { get; }
    T? Find(object? id);
    ValueTask<T> FindAsync(object id, CancellationToken cancellationToken = default);
    T? First(Expression<Func<T, bool>> expression);
    Task<T> FirstAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
    T? FirstOrDefault(Expression<Func<T, bool>> expression);
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>>? expression, CancellationToken cancellationToken = default);
    T Single(Expression<Func<T, bool>> expression);
    Task<T> SingleAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
    public Task<IQueryable<T>> GetAll();
    public Task<T?> GetByIdAsync(int id);
    public ValueTask AddAsync(T entity);
    public ValueTask AddRangeAsync(List<T> entity);
    public ValueTask UpdateAsync(T entity);
    public ValueTask Remove(T entity);
    public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    public bool StateChanged();
}
