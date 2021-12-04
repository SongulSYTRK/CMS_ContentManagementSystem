using CMS.Domain.Entities.Interface;
using CMS.Domain.Repositories.Interface.BaseRepositories;
using CMS.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Infrastructure.Repositories.Abstract
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class, IBaseEntity
    {

        private readonly AppDbContext _db;
        protected DbSet<T> table;
        public BaseRepository(AppDbContext db)
        {
            _db = db;
            table = _db.Set<T>();
        }

        public async Task Add(T entity) => await table.AddAsync(entity);

        public async Task<bool> Any(System.Linq.Expressions.Expression<Func<T, bool>> expression) => await table.AnyAsync(expression);


        public void Delete(T entity)
        {
            //=> table.Remove(entity);
        }

        public void Update(T entity) => _db.Entry<T>(entity).State = EntityState.Modified;

        public async Task<T> GetByDefault(System.Linq.Expressions.Expression<Func<T, bool>> expression) => await table.FirstOrDefaultAsync(expression);


        public async Task<List<T>> GetByDefaults(System.Linq.Expressions.Expression<Func<T, bool>> expression) => await table.Where(expression).ToListAsync();

        public async Task<TResult> GetFilteredFirstOrDefault<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> expression, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = table;
            if (include != null)
            {
                query = include(query);
            }
            if (expression != null)
            {
                query = query.Where(expression);
            }
            if (orderBy != null)
            {
                return await orderBy(query).Select(selector).FirstOrDefaultAsync();
            }
            else
            {
                return await query.Select(selector).FirstOrDefaultAsync();
            }
        }

        public async Task<List<TResult>> GetFilteredFirstOrDefaults<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> expression, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = table;
            if (include != null)
            {
                query = include(query);
            }
            if (expression != null)
            {
                query = query.Where(expression);
            }
            if (orderBy != null)
            {
                return await orderBy(query).Select(selector).ToListAsync();
            }
            else
            {
                return await query.Select(selector).ToListAsync();
            }

        }
    }
}
