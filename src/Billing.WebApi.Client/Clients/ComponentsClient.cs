using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Billing.WebApi.Client.Models;
using Billing.WebApi.Client.Utility;
using Microsoft.AspNetCore.WebUtilities;

namespace Billing.WebApi.Client.Clients
{
    public class ComponentsClient
    {
        public string BaseUrl { get; }
        private static readonly string apiUrl = "api/components";

        public ComponentsClient(string baseUrl)
        {
            BaseUrl = baseUrl;
        }

        public async Task<Result<GetComponentDto>> CreateComponentAsync(CreateComponentDto componentToCreate)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PostAsJsonAsync($"{BaseUrl}/{apiUrl}", componentToCreate);
                return response.IsSuccessStatusCode 
                    ? await response.Content.ReadAsAsync<Result<GetComponentDto>>() 
                    : new Result<GetComponentDto>
                    {
                        IsSuccess = false,
                        Message = ReasonPhrases.GetReasonPhrase(Convert.ToInt32(response.StatusCode))
                    };
            }
        }

        public async Task<Result<GetComponentDto>> GetComponentAsync(Guid componentId)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{BaseUrl}/{apiUrl}/{componentId}");
                return response.IsSuccessStatusCode
                    ? await response.Content.ReadAsAsync<Result<GetComponentDto>>()
                    : new Result<GetComponentDto>
                    {
                        IsSuccess = false,
                        Message = ReasonPhrases.GetReasonPhrase(Convert.ToInt32(response.StatusCode))
                    };
            }
        }

        public async Task<Result<List<GetComponentDto>>> GetComponentsAsync()
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{BaseUrl}/{apiUrl}");
                return response.IsSuccessStatusCode
                    ? await response.Content.ReadAsAsync<Result<List<GetComponentDto>>>()
                    : new Result<List<GetComponentDto>>
                    {
                        IsSuccess = false,
                        Message = ReasonPhrases.GetReasonPhrase(Convert.ToInt32(response.StatusCode))
                    };
            }
        }

        public async Task<Result<GetComponentDto>> DeleteComponentAsync(Guid componentId)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.DeleteAsync($"{BaseUrl}/{apiUrl}/{componentId}");
                return response.IsSuccessStatusCode
                    ? await response.Content.ReadAsAsync<Result<GetComponentDto>>()
                    : new Result<GetComponentDto>
                    {
                        IsSuccess = false,
                        Message = ReasonPhrases.GetReasonPhrase(Convert.ToInt32(response.StatusCode))
                    };
            }
        }
    }
}
