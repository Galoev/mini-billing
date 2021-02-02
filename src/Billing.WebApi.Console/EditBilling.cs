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

        private static readonly string serviceAddress = "https://localhost:44311";

        public EditBilling()
        {
            customersClient = new CustomersClient(serviceAddress);
            ordersClient = new OrdersClient(serviceAddress);
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
    }
}
