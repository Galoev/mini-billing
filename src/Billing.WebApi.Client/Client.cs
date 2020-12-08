using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Billing.WebApi.Client.Models;

namespace Billing.WebApi.Client
{
    public class Client : IClient
    {
        public string BaseUrl { get; }
        private static HttpClient httpClient = new HttpClient();

        public Client(string baseUrl)
        {
            BaseUrl = baseUrl;
        }        

        public async Task<OrderDto> GetOrderAsync(int id)
        {
            UriBuilder builder = new UriBuilder(BaseUrl + "/api/simple/order");
            builder.Query = $"id={id}";
            OrderDto order = null;
            HttpResponseMessage response = await httpClient.GetAsync(builder.Uri);
            if (response.IsSuccessStatusCode)
            {
                order = await response.Content.ReadAsAsync<OrderDto>();
            }
            return order;
        }

        public async Task<List<OrderDto>> GetOrdersAsync()
        {
            UriBuilder builder = new UriBuilder(BaseUrl + "/api/simple/orders");
            List<OrderDto> orders = null;
            HttpResponseMessage response = await httpClient.GetAsync(builder.Uri);
            if (response.IsSuccessStatusCode)
            {
                orders = await response.Content.ReadAsAsync<List<OrderDto>>();
            }
            return orders;
        }

        public async Task<List<ProductDto>> GetProductsAsync()
        {
            UriBuilder builder = new UriBuilder(BaseUrl + "/api/simple/products");
            List<ProductDto> products = null;
            HttpResponseMessage response = await httpClient.GetAsync(builder.Uri);
            if (response.IsSuccessStatusCode)
            {
                products = await response.Content.ReadAsAsync<List<ProductDto>>();
            }
            return products;
        }

        public async Task<List<CustomerDto>> GetCustomersAsync()
        {
            UriBuilder builder = new UriBuilder(BaseUrl + "/api/simple/customers");
            List<CustomerDto> customers = null;
            HttpResponseMessage response = await httpClient.GetAsync(builder.Uri);
            if (response.IsSuccessStatusCode)
            {
                customers = await response.Content.ReadAsAsync<List<CustomerDto>>();
            }
            return customers;
        }

        public async Task<Uri> CreateCustomerAsync(CustomerDto customer)
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync(BaseUrl + "api/simple/create/customer", customer);
            response.EnsureSuccessStatusCode();

            return response.Headers.Location;
        }

        public async Task<Uri> CreateOrderAsync(OrderDto order)
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync("api/simple/create/order", order);
            response.EnsureSuccessStatusCode();

            return response.Headers.Location;
        }
    }
}
