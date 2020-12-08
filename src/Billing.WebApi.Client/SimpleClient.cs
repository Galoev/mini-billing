using Billing.WebApi.Client.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Billing.WebApi.Client
{
    public class SimpleClient
    {
        private string _baseUrl = "";

        public SimpleClient(string baseUrl)
        {
            BaseUrl = baseUrl;
        }

        public string BaseUrl
        {
            get { return _baseUrl; }
            private set { _baseUrl = value; }
        }

        public async Task<MessageDto> GetMessageAsync(string name)
        {
            using (var client = new HttpClient())
            {
                var builder = new UriBuilder(BaseUrl + "/api/simple");
                builder.Query = $"name={name}";

                HttpResponseMessage response = await client.GetAsync(builder.Uri);
                var messageDto = response.IsSuccessStatusCode ? await response.Content.ReadAsAsync<MessageDto>() : null;
                return messageDto;
            }
        }
    }
}
