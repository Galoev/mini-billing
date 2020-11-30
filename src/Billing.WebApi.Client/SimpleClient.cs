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
            set { _baseUrl = value; }
        }

        public async Task<Message> GetMessageAsync(string name)
        {
            using (var client = new HttpClient())
            {
                UriBuilder builder = new UriBuilder(BaseUrl + "/api/simple");
                builder.Query = $"name={name}";

                MessageDTO message = null;
                HttpResponseMessage response = await client.GetAsync(builder.Uri);
                if (response.IsSuccessStatusCode)
                {
                    message = await response.Content.ReadAsAsync<MessageDTO>();
                }
                return messageFromDTO(message);
            }
        }

        private static Message messageFromDTO(MessageDTO messageDTO) =>
            new Message
            {
                message = messageDTO.message
            };
    }
}
