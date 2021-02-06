using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Billing.WebApi.Client.Models;
using Billing.WebApi.Client.Utility;

namespace Billing.WebApi.Client.Clients
{
    public class OrdersClient
    {
        public string BaseUrl { get; }
        private static readonly string apiUrl = "api/orders";

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
                HttpResponseMessage response = await client.PostAsJsonAsync($"{BaseUrl}/{apiUrl}", orderToCreate);
                return response.IsSuccessStatusCode 
                    ? await response.Content.ReadAsAsync<Result<GetOrderDto>>() 
                    : new Result<GetOrderDto> 
                    { 
                        IsSuccess = false, 
                        Message = ReasonPhrases.GetReasonPhrase(Convert.ToInt32(response.StatusCode))
                    };
            }
        }

        public async Task<Result<GetOrderDto>> GetOrderAsync(Guid orderId)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{BaseUrl}/{apiUrl}/{orderId}");
                return response.IsSuccessStatusCode 
                    ? await response.Content.ReadAsAsync<Result<GetOrderDto>>() 
                    : new Result<GetOrderDto> 
                    { 
                        IsSuccess = false, 
                        Message = ReasonPhrases.GetReasonPhrase(Convert.ToInt32(response.StatusCode))
                    };
            }
        }

        public async Task<Result<List<GetOrderDto>>> GetOrdersAsync()
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{BaseUrl}/{apiUrl}");
                return response.IsSuccessStatusCode
                    ? await response.Content.ReadAsAsync<Result<List<GetOrderDto>>>()
                    : new Result<List<GetOrderDto>> 
                    { 
                        IsSuccess = false, 
                        Message = ReasonPhrases.GetReasonPhrase(Convert.ToInt32(response.StatusCode))
                    };
            }
        }

        public async Task<Result<GetOrderDto>> UpdateOrderAsync(CreateOrderDto orderToUpdate)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PutAsJsonAsync($"{BaseUrl}/{apiUrl}", orderToUpdate);
                return response.IsSuccessStatusCode 
                    ? await response.Content.ReadAsAsync<Result<GetOrderDto>>() 
                    : new Result<GetOrderDto> 
                    { 
                        IsSuccess = false, 
                        Message = ReasonPhrases.GetReasonPhrase(Convert.ToInt32(response.StatusCode)) 
                    };
            }
        }

        public async Task<Result<GetOrderDto>> DeleteOrderAsync(Guid orderId)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.DeleteAsync($"{BaseUrl}/{apiUrl}/{orderId}");
                return response.IsSuccessStatusCode 
                    ? await response.Content.ReadAsAsync<Result<GetOrderDto>>() 
                    : new Result<GetOrderDto> 
                    { 
                        IsSuccess = false, 
                        Message = ReasonPhrases.GetReasonPhrase(Convert.ToInt32(response.StatusCode))
                    };
            }
        }
    }
}
