using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Billing.WebApi.Client.Clients;
using Billing.WebApi.Console.Converters;
using Billing.WebApi.Console.Models;

namespace Billing.WebApi.Console
{
    public class SearchBilling: ISearchBilling
    {
        private readonly CustomersClient customersClient;
        private readonly OrdersClient ordersClient;
        private readonly GoodsClient goodsClient;

        private static readonly string serviceAddress = "https://localhost:44311";

        public SearchBilling()
        {
            customersClient = new CustomersClient(serviceAddress);
            ordersClient = new OrdersClient(serviceAddress);
            goodsClient = new GoodsClient(serviceAddress);
        }

        public async Task<List<Customer>> GetCustomers()
        {
            var resultFromClient = await customersClient.GetCustomersAsync();
            if (resultFromClient.IsSuccess)
            {
                return resultFromClient.Value.Select(c => CustomerConverter.FromDto(c)).ToList();
            }
            return Enumerable.Empty<Customer>().ToList();
        }

        public async Task<List<Good>> GetGoods()
        {
            var resultFromClient = await goodsClient.GetGoodsAsync();
            if (resultFromClient.IsSuccess)
            {
                return resultFromClient.Value.Select(g => GoodConverter.FromDto(g)).ToList();
            }
            return Enumerable.Empty<Good>().ToList();
        }

        public async Task<List<GoodInfo>> GetInfoGoods()
        {
            var resultFromClient = await goodsClient.GetGoodsAsync();
            if (resultFromClient.IsSuccess)
            {
                return resultFromClient.Value.Select(g => GoodConverter.InfoGoodFromDto(g)).ToList();
            }
            return Enumerable.Empty<GoodInfo>().ToList();
        }

        public async Task<List<Order>> GetOrders()
        {
            var resultFromClient = await ordersClient.GetOrdersAsync();
            if (resultFromClient.IsSuccess)
            {
                return resultFromClient.Value.Select(o => OrderConverter.FromDto(o)).ToList();
            }
            return Enumerable.Empty<Order>().ToList();
        }

        public async Task<List<OrderInfo>> GetInfoOrders()
        {
            var resultFromClient = await ordersClient.GetOrdersAsync();
            if (resultFromClient.IsSuccess)
            {
                return resultFromClient.Value.Select(o => OrderConverter.InfoOrderFromDto(o)).ToList();
            }
            return Enumerable.Empty<OrderInfo>().ToList();
        }
    }
}
