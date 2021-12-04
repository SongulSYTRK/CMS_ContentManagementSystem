using CMS.Domain.Entities.Interface;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Domain.Repositories.Interface.BaseRepositories
{
   public  interface IBaseRepository<T> where T:IBaseEntity
    {
        Task Add(T entity);
        void Delete(T entity);

        void Update(T entity);

        Task<T> GetByDefault(Expression<Func<T, bool>> expression);
        Task<List<T>> GetByDefaults(Expression<Func<T, bool>> expression);

        Task<bool> Any(Expression<Func<T, bool>> expression);



        //normally unitofwork dont prefer getquery. But I am learning step by step . so I used here .
        Task<TResult> GetFilteredFirstOrDefault<TResult>(Expression<Func<T, TResult>> selector,
                                                         Expression<Func<T, bool>> expression,
                                                         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                         Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

        Task<List<TResult>> GetFilteredFirstOrDefaults<TResult>(Expression<Func<T, TResult>> selector,
                                                       Expression<Func<T, bool>> expression,
                                                       Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                       Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
    }
}
