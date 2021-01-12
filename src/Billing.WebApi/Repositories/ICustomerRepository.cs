using Billing.WebApi.Client.Utility;
using Billing.WebApi.Models;
using System;
using System.Collections.Generic;

namespace Billing.WebApi.Repositories
{
    public interface ICustomerRepository
    {
        Result<Customer> Create(Customer customerToCreate);

        Result<Customer> Get(Guid customerId);

        Result<List<Customer>> Get();

        Result<Customer> Update(Customer customerToUpdate);

        Result<Customer> Delete(Guid id);
    }
}
