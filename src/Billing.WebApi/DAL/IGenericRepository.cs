using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Billing.WebApi.DAL
{
    public interface IGenericRepository<T> where T : class
    {
        T Get(object id);
        List<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy= null);
        void Delete(object id);

        void Delete(T entity);
        void Add(T entity);
        void Update(T entity);
    } 
}
