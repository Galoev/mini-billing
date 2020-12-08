using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Billing.WebApi.Client.Models;

namespace Billing.WebApi.Client
{
    public interface IClient
    {                
        Task<OrderDto> GetOrderAsync(int id);
        Task<List<OrderDto>> GetOrdersAsync();
        Task<List<ProductDto>> GetProductsAsync();
        Task<List<CustomerDto>> GetCustomersAsync();
        Task<Uri> CreateCustomerAsync(CustomerDto customer);
        Task<Uri> CreateOrderAsync(OrderDto order);
    }
}
