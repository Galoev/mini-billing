using System;

using Billing.WebApi.Models;
using Billing.WebApi.Client.Utility;

namespace Billing.WebApi.Repositories
{
    public interface IOrdersRepository
    {
        Result<Order> Create(Order orderToCreate);

        Result<Order> Get(Guid orderId);

        Result<Order> Update(Order orderToUpdate);

        Result<Order> Delete(Guid id);
    }
}
