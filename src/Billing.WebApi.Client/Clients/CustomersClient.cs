using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Billing.WebApi.Client.Models;
using Billing.WebApi.Client.Utility;

namespace Billing.WebApi.Client.Clients
{
    public class CustomersClient
    {
        public string BaseUrl { get; }
        private static readonly string apiUrl = "api/customers";

        public CustomersClient(string baseUrl)
        {
            BaseUrl = baseUrl;
        }

        public async Task<Result<CustomerDto>> CreateCustomerAsync(CustomerDto customerToCreate)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PostAsJsonAsync($"{BaseUrl}/{apiUrl}", customerToCreate);
                return response.IsSuccessStatusCode ? await response.Content.ReadAsAsync<Result<CustomerDto>>() : null;
            }
        }

        public async Task<Result<List<CustomerDto>>> GetCustomersAsync()
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{BaseUrl}/{apiUrl}");
                return response.IsSuccessStatusCode ? await response.Content.ReadAsAsync<Result<List<CustomerDto>>>() : null;
            }
        }

        public async Task<Result<CustomerDto>> GetCustomerAsync(Guid customerId)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{BaseUrl}/{apiUrl}/{customerId}");
                return response.IsSuccessStatusCode ? await response.Content.ReadAsAsync<Result<CustomerDto>>() : null;
            }
        }

        public async Task<Result<CustomerDto>> DeleteCustomerAsync(Guid customerId)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.DeleteAsync($"{BaseUrl}/{apiUrl}/{customerId}");
                return response.IsSuccessStatusCode ? await response.Content.ReadAsAsync<Result<CustomerDto>>() : null;
            }
        }

        public async Task<Result<CustomerDto>> UpdateCustomerAsync(CustomerDto customerToUpdate)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PutAsJsonAsync($"{BaseUrl}/{apiUrl}", customerToUpdate);
                return response.IsSuccessStatusCode ? await response.Content.ReadAsAsync<Result<CustomerDto>>() : null;
            }
        }
    }
}
