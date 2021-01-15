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

                Result<CustomerDto> result = null;
                HttpResponseMessage response = await client.PostAsJsonAsync(BaseUrl + "/api/customers", customerToCreate);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<Result<CustomerDto>>();
                }
                return result;
            }
        }

        public async Task<Result<List<CustomerDto>>> GetCustomersAsync()
        {
            using (var client = new HttpClient())
            {
                Result<List<CustomerDto>> result = null;
                HttpResponseMessage response = await client.GetAsync(BaseUrl + "/api/customers");
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<Result<List<CustomerDto>>>();
                }
                return result;
            }
        }

        public async Task<Result<CustomerDto>> GetCustomerAsync(Guid customerId)
        {
            using (var client = new HttpClient())
            {
                Result<CustomerDto> result = null;
                HttpResponseMessage response = await client.GetAsync(BaseUrl + $"/api/customers/{customerId}");
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<Result<CustomerDto>>();
                }
                return result;
            }
        }

        public async Task<Result<CustomerDto>> DeleteCustomerAsync(Guid customerId)
        {
            using (var client = new HttpClient())
            {
                Result<CustomerDto> result = null;
                HttpResponseMessage response = await client.DeleteAsync(BaseUrl + $"/api/customers/{customerId}");
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<Result<CustomerDto>>();
                }
                return result;
            }
        }

        public async Task<Result<CustomerDto>> UpdateCustomerAsync(CustomerDto customerToUpdate)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Result<CustomerDto> result = null;
                HttpResponseMessage response = await client.PutAsJsonAsync(BaseUrl + "/api/customers", customerToUpdate);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<Result<CustomerDto>>();
                }
                return result;
            }
        }
    }
}
