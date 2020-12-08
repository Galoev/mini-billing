using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Billing.WebApi.Models;

namespace Billing.WebApi.DAL
{
    public interface IOrdersRepository
    {
        decimal Create(Order orderToCreate);

        List<Order> Get(Expression<Func<Order, bool>> filter = null, 
            Func<IQueryable<Order>, IOrderedQueryable<Order>> orderBy = null);

        List<Component> GetComponents();

        Order Get(int orderId);

        void Update(Order orderToUpdate);

        void Delete(Order orderToDelete);

        void Delete(int id);
    }
}
