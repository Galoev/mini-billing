using Billing.WebApi.Models;
using System;

namespace Billing.WebApi.Repositories
{
    public interface IOrdersRepository
    {
        decimal Create(Order orderToCreate);

        OrderRepositoryResult Get(Guid orderId);

        void Update(Order orderToUpdate);

        void Delete(Order orderToDelete);

        void Delete(Guid id);
    }
}
