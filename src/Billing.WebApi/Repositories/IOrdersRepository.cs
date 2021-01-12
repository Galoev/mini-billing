using System;

using Billing.WebApi.Models;
using Billing.WebApi.Client.Utility;
using System.Collections.Generic;

namespace Billing.WebApi.Repositories
{
    public interface IOrdersRepository
    {
        Result<Order> Create(Order orderToCreate);

        Result<Order> Get(Guid orderId);

        Result<List<Order>> Get();

        Result<Order> Update(Order orderToUpdate);

        Result<Order> Delete(Guid id);
    }
}
