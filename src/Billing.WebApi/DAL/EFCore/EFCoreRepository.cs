using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Billing.WebApi.DAL.EFCore
{
    public class EFCoreRepository<T> : IGenericRepository<T> where T : class
    {
        private BillingContext context;
        private DbSet<T> dbSet;

        public EFCoreRepository(BillingContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(object id)
        {
            T entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public void Delete(T entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public T Get(object id)
        {
            return dbSet.Find(id);
        }

        public List<T> Get(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                // order => order.Price > 400
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                // q => q.OrderBy(s => s.Price)
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public void Update(T entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }
    }
}
