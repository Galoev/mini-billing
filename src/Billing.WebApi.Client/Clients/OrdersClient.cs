using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Billing.WebApi.Client.Models;
using Billing.WebApi.Client.Utility;

namespace Billing.WebApi.Client.Clients
{
    public class OrdersClient
    {
        public string BaseUrl { get; }

        public OrdersClient(string baseUrl)
        {
            BaseUrl = baseUrl;
        }

        public async Task<Result<GetOrderDto>> CreateOrderAsync(CreateOrderDto orderToCreate)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Result<GetOrderDto> result = null;
                HttpResponseMessage response = await client.PostAsJsonAsync(BaseUrl + "/api/orders", orderToCreate);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<Result<GetOrderDto>>();
                }
                return result;
            }
        }

        public async Task<Result<List<GetOrderDto>>> GetOrdersAsync()
        {
            using (var client = new HttpClient())
            {
                Result<List<GetOrderDto>> result = null;
                HttpResponseMessage response = await client.GetAsync(BaseUrl + "/api/orders");
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<Result<List<GetOrderDto>>>();
                }
                return result;
            }
        }
    }
}
