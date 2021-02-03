using Billing.WebApi.Client.Utility;
using Billing.WebApi.Models;
using Billing.WebApi.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Billing.WebApi.Repositories
{
    public class CustomersRespository : ICustomersRepository
    {
        private readonly BillingContext billingContext;

        public CustomersRespository(BillingContext billingContext)
        {
            this.billingContext = billingContext;
        }

        public Result<Customer> Create(Customer customerToCreate)
        {
            var customerDbo = new CustomerDbo
            {
                Phone = customerToCreate.Phone,
                Name = customerToCreate.Name,
                AdditionalInfo = customerToCreate.AdditionalInfo
            };

            billingContext.Customers.AddIfNotExists(customerDbo, c => c.Phone == customerDbo.Phone);
            var createdRows = billingContext.SaveChanges();
            if (createdRows > 0)
            {
                customerToCreate.Id = customerDbo.Id;
                return new Result<Customer>
                {
                    IsSuccess = true,
                    Message = "Customer successfully created!",
                    Value = customerToCreate
                };
            }

            return new Result<Customer>
            {
                IsSuccess = false,
                Message = "Customer not created!"
            };
        }

        public Result<Customer> Delete(Guid id)
        {
            var customerDbo = billingContext.Customers.FirstOrDefault(c => c.Id == id);
            if (customerDbo == null)
            {
                return new Result<Customer>
                {
                    IsSuccess = false,
                    Message = $"Customer with id {id} not found!",
                };
            }

            var deletedCustomer = new Customer
            {
                Id = customerDbo.Id,
                Phone = customerDbo.Phone,
                Name = customerDbo.Name,
                AdditionalInfo = customerDbo.AdditionalInfo
            };

            billingContext.Customers.Remove(customerDbo);
            var deletedRows = billingContext.SaveChanges();

            if (deletedRows > 0)
            {
                return new Result<Customer>
                {
                    IsSuccess = true,
                    Message = "Customer successfully deleted!",
                    Value = deletedCustomer
                };
            }
            return new Result<Customer>
            {
                IsSuccess = false,
                Message = "Customer not deleted!"
            };
        }

        // возможно, следует получать по номеру телефона, а не id
        public Result<Customer> Get(Guid customerId)
        {
            var customerDbo = billingContext.Customers.FirstOrDefault(c => c.Id == customerId);
            if (customerDbo == null)
            {
                return new Result<Customer>
                {
                    IsSuccess = false,
                    Message = $"Could not find customer with id {customerId}!"
                };
            }

            return new Result<Customer>
            {
                IsSuccess = true,
                Message = $"Customer with id {customerId} successfully found!",
                Value = new Customer
                {
                    Id = customerDbo.Id,
                    Phone = customerDbo.Phone,
                    Name = customerDbo.Name,
                    AdditionalInfo = customerDbo.AdditionalInfo
                }
            };
        }

        public Result<List<Customer>> GetAll()
        {
            var listOfCustomers = billingContext.Customers.Select(c => new Customer
            {
                Id = c.Id,
                Phone = c.Phone,
                Name = c.Name,
                AdditionalInfo = c.AdditionalInfo
            }).ToList();
            return new Result<List<Customer>>
            {
                IsSuccess = true,
                Message = "List of customers successfully found!",
                Value = listOfCustomers
            };
        }

        public Result<Customer> Update(Customer customerToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
