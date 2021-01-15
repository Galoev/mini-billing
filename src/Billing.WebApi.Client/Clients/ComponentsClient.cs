using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Billing.WebApi.Client.Models;
using Billing.WebApi.Client.Utility;

namespace Billing.WebApi.Client.Clients
{
    public class ComponentsClient
    {
        public string BaseUrl { get; }

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

                Result<GetComponentDto> result = null;
                HttpResponseMessage response = await client.PostAsJsonAsync(BaseUrl + "/api/components", componentToCreate);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<Result<GetComponentDto>>();
                }
                return result;
            }
        }

        public async Task<Result<GetComponentDto>> GetComponentAsync(Guid componentId)
        {
            using (var client = new HttpClient())
            {
                Result<GetComponentDto> result = null;
                HttpResponseMessage response = await client.GetAsync(BaseUrl + $"/api/components/{componentId}");
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<Result<GetComponentDto>>();
                }
                return result;
            }
        }

        public async Task<Result<GetComponentDto>> DeleteComponentAsync(Guid componentId)
        {
            using (var client = new HttpClient())
            {
                Result<GetComponentDto> result = null;
                HttpResponseMessage response = await client.DeleteAsync(BaseUrl + $"/api/components/{componentId}");
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<Result<GetComponentDto>>();
                }
                return result;
            }
        }
    }
}
