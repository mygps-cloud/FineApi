using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Fine.Api.DataAccess.Contracts.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<IQueryable<T>> GetAllAsync();
        public Task<T?> GetByIdAsync(int id);
        public ValueTask UpdateAsync(T entity);
        public Task<T?> FindAsync(Expression<Func<T, bool>> predicate);
        public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    }
}
