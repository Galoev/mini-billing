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

                Result<GetGoodDto> result = null;
                HttpResponseMessage response = await client.PostAsJsonAsync(BaseUrl + "/api/goods", goodToCreate);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<Result<GetGoodDto>>();
                }
                return result;
            }
        }

        public async Task<Result<GetGoodDto>> GetGoodAsync(Guid goodId)
        {
            using (var client = new HttpClient())
            {
                Result<GetGoodDto> result = null;
                HttpResponseMessage response = await client.GetAsync(BaseUrl + $"/api/goods/{goodId}");
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<Result<GetGoodDto>>();
                }
                return result;
            }
        }

        public async Task<Result<List<GetGoodDto>>> GetGoodsAsync()
        {
            using (var client = new HttpClient())
            {
                Result<List<GetGoodDto>> result = null;
                HttpResponseMessage response = await client.GetAsync(BaseUrl + "/api/goods");
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<Result<List<GetGoodDto>>>();
                }
                return result;
            }
        }

        public async Task<Result<GetGoodDto>> UpdateGoodAsync(CreateGoodDto goodToUpdate)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Result<GetGoodDto> result = null;
                HttpResponseMessage response = await client.PutAsJsonAsync(BaseUrl + "/api/goods", goodToUpdate);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<Result<GetGoodDto>>();
                }
                return result;
            }
        }

        public async Task<Result<GetGoodDto>> DeleteGoodAsync(Guid goodId)
        {
            using (var client = new HttpClient())
            {
                Result<GetGoodDto> result = null;
                HttpResponseMessage response = await client.DeleteAsync(BaseUrl + $"/api/goods/{goodId}");
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<Result<GetGoodDto>>();
                }
                return result;
            }
        }
    }
}
