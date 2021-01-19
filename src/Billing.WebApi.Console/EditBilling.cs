using System.Threading.Tasks;
using Billing.WebApi.Client.Clients;
using Billing.WebApi.Console.Converters;
using Billing.WebApi.Console.Models;

namespace Billing.WebApi.Console
{
    public class EditBilling: IEditBilling
    {
        private readonly CustomersClient customersClient;
        private readonly OrdersClient ordersClient;

        private static readonly string serviceAddress = "https://localhost:44311";

        public EditBilling()
        {
            customersClient = new CustomersClient(serviceAddress);
            ordersClient = new OrdersClient(serviceAddress);
        }
        public async Task<Customer> CreateCustomer(Customer customer)
        {
            var resultFromClient = await customersClient.CreateCustomerAsync(CustomerConverter.ToDto(customer));
            if (resultFromClient.IsSuccess)
            {
                return CustomerConverter.FromDto(resultFromClient.Value);
            }
            return null;
        }

        public async Task<Order> CreateOrder(CreateOrder order)
        {
            var resultFromClient = await ordersClient.CreateOrderAsync(OrderConverter.ToDto(order));
            if (resultFromClient.IsSuccess)
            {
                return OrderConverter.FromDto(resultFromClient.Value);
            }
            return null;
        }
    }
}
