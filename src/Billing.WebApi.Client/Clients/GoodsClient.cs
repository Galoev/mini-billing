using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Billing.WebApi.Client.Models;
using Billing.WebApi.Client.Utility;

namespace Billing.WebApi.Client.Clients
{
    public class GoodsClient
    {
        public string BaseUrl { get; }
        private static readonly string apiUrl = "api/goods";

        public GoodsClient(string baseUrl)
        {
            BaseUrl = baseUrl;
        }

        public async Task<Result<GetGoodDto>> CreateGoodAsync(CreateGoodDto goodToCreate)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PostAsJsonAsync($"{BaseUrl}/{apiUrl}", goodToCreate);
                return response.IsSuccessStatusCode ? await response.Content.ReadAsAsync<Result<GetGoodDto>>() : null;
            }
        }

        public async Task<Result<GetGoodDto>> GetGoodAsync(Guid goodId)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{BaseUrl}/{apiUrl}/{goodId}");
                return response.IsSuccessStatusCode ? await response.Content.ReadAsAsync<Result<GetGoodDto>>() : null;
            }
        }

        public async Task<Result<List<GetGoodDto>>> GetGoodsAsync()
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{BaseUrl}/{apiUrl}");
                return response.IsSuccessStatusCode ? await response.Content.ReadAsAsync<Result<List<GetGoodDto>>>() : null;
            }
        }

        public async Task<Result<GetGoodDto>> UpdateGoodAsync(CreateGoodDto goodToUpdate)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PutAsJsonAsync($"{BaseUrl}/{apiUrl}", goodToUpdate);
                return response.IsSuccessStatusCode ? await response.Content.ReadAsAsync<Result<GetGoodDto>>() : null;
            }
        }

        public async Task<Result<GetGoodDto>> DeleteGoodAsync(Guid goodId)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.DeleteAsync($"{BaseUrl}/{apiUrl}/{goodId}");
                return response.IsSuccessStatusCode ? await response.Content.ReadAsAsync<Result<GetGoodDto>>() : null;
            }
        }
    }
}
