using Fine.Api.DataAccess.Contracts.Repositories;
using Fine.Api.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Fine.Api.DataAcess.DbContexts;

namespace Fine.Api.DataAcess.Repositories
{
    public class GenericRepository<T>:IGenericRepository<T> where T : class
    {
        private readonly FineDbContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(FineDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<IQueryable<T>> GetAllAsync()
        {
            return await Task.FromResult(_dbSet.AsNoTracking());
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public ValueTask UpdateAsync(T entity)
        {
                if (entity == null) throw new NoReceivedSmsOnThisReceiptNumberException();
                _dbSet.Attach(entity);
                var item = _context.Entry(entity);
                item.State = EntityState.Modified;
                item.CurrentValues.SetValues(entity);
                return ValueTask.CompletedTask;
        }

        async Task<bool> IGenericRepository<T>.AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        async Task<T> IGenericRepository<T?>.FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }
    }
}
