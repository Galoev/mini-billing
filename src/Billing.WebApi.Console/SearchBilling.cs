using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Billing.WebApi.Client.Clients;
using Billing.WebApi.Client.Utility;
using Billing.WebApi.Console.Converters;
using Billing.WebApi.Console.Models;

namespace Billing.WebApi.Console
{
    public class SearchBilling : ISearchBilling
    {
        private readonly CustomersClient customersClient;
        private readonly OrdersClient ordersClient;
        private readonly GoodsClient goodsClient;
        private readonly ComponentsClient componentsClient;

        private static readonly string serviceAddress = "https://localhost:44311";

        public SearchBilling()
        {
            customersClient = new CustomersClient(serviceAddress);
            ordersClient = new OrdersClient(serviceAddress);
            goodsClient = new GoodsClient(serviceAddress);
            componentsClient = new ComponentsClient(serviceAddress);
        }

        public async Task<Result<List<Customer>>> GetCustomers()
        {
            var resultFromClient = await customersClient.GetCustomersAsync();
            return new Result<List<Customer>>
            {
                    IsSuccess = resultFromClient.IsSuccess,
                    Message = resultFromClient.Message,
                    Value = resultFromClient.IsSuccess 
                        ? resultFromClient.Value.Select(c => CustomerConverter.FromDto(c)).ToList()
                        : null
            };
        }

        public async Task<Result<List<Component>>> GetComponents()
        {
            var resultFromClient = await componentsClient.GetComponentsAsync();
            return new Result<List<Component>>
            {
                IsSuccess = resultFromClient.IsSuccess,
                Message = resultFromClient.Message,
                Value = resultFromClient.IsSuccess
                    ? resultFromClient.Value.Select(c => ComponentConverter.FromDto(c)).ToList()
                    : null
            };
        }

        public async Task<Result<List<Good>>> GetGoods()
        {
            var resultFromClient = await goodsClient.GetGoodsAsync();
            return new Result<List<Good>>
            {
                IsSuccess = resultFromClient.IsSuccess,
                Message = resultFromClient.Message,
                Value = resultFromClient.IsSuccess
                    ? resultFromClient.Value.Select(g => GoodConverter.FromDto(g)).ToList()
                    : null
            };
        }

        public async Task<Result<List<GoodInfo>>> GetGoodsInfo()
        {
            var resultFromClient = await goodsClient.GetGoodsAsync();
            return new Result<List<GoodInfo>>
            {
                IsSuccess = resultFromClient.IsSuccess,
                Message = resultFromClient.Message,
                Value = resultFromClient.IsSuccess
                    ? resultFromClient.Value.Select(g => GoodConverter.InfoGoodFromDto(g)).ToList()
                    : null
            };
        }

        public async Task<Result<List<Order>>> GetOrders()
        {
            var resultFromClient = await ordersClient.GetOrdersAsync();
            return new Result<List<Order>>
            {
                IsSuccess = resultFromClient.IsSuccess,
                Message = resultFromClient.Message,
                Value = resultFromClient.IsSuccess
                    ? resultFromClient.Value.Select(o => OrderConverter.FromDto(o)).ToList()
                    : null
            };
        }

        public async Task<Result<List<OrderInfo>>> GetOrdersInfo()
        {
            var resultFromClient = await ordersClient.GetOrdersAsync();
            return new Result<List<OrderInfo>>
            {
                IsSuccess = resultFromClient.IsSuccess,
                Message = resultFromClient.Message,
                Value = resultFromClient.IsSuccess
                    ? resultFromClient.Value.Select(o => OrderConverter.InfoOrderFromDto(o)).ToList()
                    : null
            };
        }
    }
}
