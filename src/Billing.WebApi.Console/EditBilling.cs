using System.Threading.Tasks;
using Billing.WebApi.Client.Clients;
using Billing.WebApi.Client.Utility;
using Billing.WebApi.Console.Converters;
using Billing.WebApi.Console.Models;

namespace Billing.WebApi.Console
{
    public class EditBilling : IEditBilling
    {
        private readonly CustomersClient customersClient;
        private readonly OrdersClient ordersClient;
        private readonly GoodsClient goodsClient;
        private readonly ComponentsClient componentsClient;

        private static readonly string serviceAddress = "https://localhost:44311";

        public EditBilling()
        {
            customersClient = new CustomersClient(serviceAddress);
            ordersClient = new OrdersClient(serviceAddress);
            goodsClient = new GoodsClient(serviceAddress);
            componentsClient = new ComponentsClient(serviceAddress);
        }
        public async Task<Result<Customer>> CreateCustomer(Customer customer)
        {
            var resultFromClient = await customersClient.CreateCustomerAsync(CustomerConverter.ToDto(customer));
            return new Result<Customer>
            {
                IsSuccess = resultFromClient.IsSuccess,
                Message = resultFromClient.Message,
                Value = resultFromClient.IsSuccess
                    ? CustomerConverter.FromDto(resultFromClient.Value)
                    : null
            };
        }

        public async Task<Result<Order>> CreateOrder(CreateOrder order)
        {
            var resultFromClient = await ordersClient.CreateOrderAsync(OrderConverter.ToDto(order));
            return new Result<Order>
            {
                IsSuccess = resultFromClient.IsSuccess,
                Message = resultFromClient.Message,
                Value = resultFromClient.IsSuccess
                    ? OrderConverter.FromDto(resultFromClient.Value)
                    : null
            };
        }

        public async Task<Result<Good>> CreateGood(CreateGood good)
        {
            var resultFromClient = await goodsClient.CreateGoodAsync(GoodConverter.ToDto(good));
            return new Result<Good>
            {
                IsSuccess = resultFromClient.IsSuccess,
                Message = resultFromClient.Message,
                Value = resultFromClient.IsSuccess
                    ? GoodConverter.FromDto(resultFromClient.Value)
                    : null
            };
        }

        public async Task<Result<Component>> CreateComponent(CreateComponent component)
        {
            var resultFromClient = await componentsClient.CreateComponentAsync(ComponentConverter.ToDto(component));
            return new Result<Component>
            {
                IsSuccess = resultFromClient.IsSuccess,
                Message = resultFromClient.Message,
                Value = resultFromClient.IsSuccess
                    ? ComponentConverter.FromDto(resultFromClient.Value)
                    : null
            };
        }
    }
}
