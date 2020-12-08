using System;
using System.Collections.Generic;
using Billing.WebApi.Client.Models;

namespace Billing.WebApi.Console
{
    public class Console: IConsole
    {
        private static Client.Client client;

        public Console(string baseUrl)
        {
            client = new Client.Client(baseUrl);
        }

        public void Run()
        {
            int variant;
            do
            {
                PrintMenu();
                variant = GetVariant(4);
                switch (variant)
                {
                    case 1:
                        CreateOrder();
                        break;
                    case 2:
                        PrintOrders(client.GetOrdersAsync().Result);
                        break;
                    case 3:
                        GetOrderInformation();
                        break;
                }

            } while (variant != 4);
        }

        private void PrintMenu()
        {
            System.Console.WriteLine("1. Create order");
            System.Console.WriteLine("2. List of all orders");
            System.Console.WriteLine("3. Order Information");
            System.Console.WriteLine("4. Exit");
            System.Console.Write(">");
        }

        private int GetVariant(int count)
        {
            int variant;
            var str = System.Console.ReadLine();

            while (!int.TryParse(str, out variant) || variant < 1 || variant > count)
            {
                System.Console.WriteLine("Incorrect input. Try again: ");
                str = System.Console.ReadLine();
            }

            return variant;
        }

        private async void CreateOrder()
        {
            List<CustomerDto> customers = client.GetCustomersAsync().Result;
            CustomerDto customer;
            if (customers != null)
            {
                System.Console.WriteLine("Choose a client:");
                PrintCustomers(customers, true);
                int variant = GetVariant(customers.Count + 1);

                if (variant == (customers.Count + 1))
                {
                    customer = await CreateCustomerAsync();
                }
                else
                {
                    customer = customers[variant - 1];
                }
            }
            else
            {
                System.Console.WriteLine("There are no clients. Create a customer.");
                customer = await CreateCustomerAsync();
            }

            var orderProducts = new List<Tuple<ProductDto, int>>();
            List<ProductDto> products = client.GetProductsAsync().Result;

            if (products == null)
            {
                System.Console.WriteLine("Error! There are no products.");
                return;
            }

            do
            {
                System.Console.WriteLine("Select a product:");
                PrintProducts(products);
                int variant = GetVariant(products.Count + 1);
                if (variant == (products.Count + 1))
                {
                    break;
                }
                int number = GetNum();
                orderProducts.Add(Tuple.Create(products[variant - 1], number));
            } while (true);

            DateTime date = GetOrderDate();
            OrderDto order = new OrderDto
            {
                Customer = customer,
                Date = date,
                Products = orderProducts
            };

            await client.CreateOrderAsync(order);
        }

        private void PrintOrders(List<OrderDto> orders, bool printExit = false)
        {
            int tableWidth = 92;
            var format = "|{0,10}|{1,10}|{2,20}|{3,15}|{4,15}|{5,15}|";

            System.Console.WriteLine(new string('-', tableWidth));
            var str = string.Format(format, "№", "Id", "Customer", "Date", "PaymentStatus", "DeliverStatus");
            System.Console.WriteLine(str);
            System.Console.WriteLine(new string('-', tableWidth));

            for (int i = 0; i < orders.Count; i++)
            {
                str = String.Format(format,
                    i + 1,
                    orders[i].Id,
                    orders[i].Customer.Name,
                    orders[i].Date.ToString("dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture),
                    orders[i].PaymentStatus,
                    orders[i].DeliverStatus
                    );
                System.Console.WriteLine(str);                
            }
            System.Console.WriteLine(new string('-', tableWidth));
            if (printExit)
            {
                System.Console.WriteLine($"{orders.Count + 1}. Exit.");
            }
            System.Console.WriteLine();
        }

        private void PrintProducts(List<ProductDto> products)
        {
            int tableWidth = 66;
            var format = "|{0,10}|{1,10}|{2,20}|{3,10}|{4,10}|";
            
            System.Console.WriteLine(new string('-', tableWidth));
            var str = string.Format(format, "№", "Id", "Name", "Price", "Measure");
            System.Console.WriteLine(str);
            System.Console.WriteLine(new string('-', tableWidth));

            for (int i = 0; i < products.Count; i++)
            {
                str = String.Format(format,
                    i + 1,
                    products[i].Id,
                    products[i].Name,
                    products[i].Price,
                    products[i].Measure
                    );
                System.Console.WriteLine(str);
            }
            System.Console.WriteLine(new string('-', tableWidth));
            System.Console.WriteLine($"{products.Count + 1}. Done.");
        }

        private void PrintCustomers(List<CustomerDto> customers, bool printCreateСlient = false)
        {
            int tableWidth = 106;
            var format = "|{0,10}|{1,10}|{2,20}|{3,20}|{4,40}|";

            System.Console.WriteLine(new string('-', tableWidth));
            var str = string.Format(format, "№", "Id", "Name", "Phone Number", "Info");
            System.Console.WriteLine(str);
            System.Console.WriteLine(new string('-', tableWidth));

            for (int i = 0; i < customers.Count; i++)
            {
                str = String.Format(format,
                    i + 1,
                    customers[i].Id,
                    customers[i].Name,
                    customers[i].PhoneNumber,
                    customers[i].Info
                    );
                System.Console.WriteLine(str);
            }
            System.Console.WriteLine(new string('-', tableWidth));
            if (printCreateСlient)
            {
                System.Console.WriteLine($"{customers.Count + 1}. Create a client.");
            }
        }

        private void PrintOrder(OrderDto order)
        {
            System.Console.WriteLine("Order information:");
            System.Console.WriteLine($"Id: {order.Id}");
            System.Console.WriteLine($"Date: {order.Date.ToString("dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture)}");
            System.Console.WriteLine($"PaymentStatus: {order.PaymentStatus}");
            System.Console.WriteLine($"DeliverStatus: {order.DeliverStatus}");
            decimal orderPrice = 0;
            var orderComponents = new Dictionary<ComponentDto, int>();
            var products = new List<ProductDto>();
            foreach (Tuple<ProductDto, int> product in order.Products)
            {
                products.Add(product.Item1);
                foreach(Tuple<ComponentDto, int> component in product.Item1.Components)
                {
                    orderComponents[component.Item1] = orderComponents.GetValueOrDefault(component.Item1, 0) + component.Item2;
                    orderPrice += component.Item1.Price * component.Item2;
                }
            }

            System.Console.WriteLine("Products:");
            PrintProducts(products);

            System.Console.WriteLine("Components:");
            int tableWidth = 86;
            var format = "|{0,10}|{1,10}|{2,20}|{3,10}|{4,10}|{5,10}|{6,10}|";

            System.Console.WriteLine(new string('-', tableWidth));
            var str = string.Format(format, "№", "Id", "Name", "Price", "Measure", "Count", "Total price");
            System.Console.WriteLine(str);
            System.Console.WriteLine(new string('-', tableWidth));
            System.Console.WriteLine();

            int index = 1;
            foreach (KeyValuePair<ComponentDto, int> component in orderComponents)
            {
                str = String.Format(format,
                    index,
                    component.Key.Id,
                    component.Key.Name,
                    component.Key.Price,
                    component.Key.Measure,
                    component.Value,
                    component.Key.Price * component.Value
                    );
                System.Console.WriteLine(str);
                index++;
            }
            str = String.Format(format, "", "", "", "", "", "", orderPrice);
            System.Console.WriteLine(str);
            System.Console.WriteLine(new string('-', tableWidth));
        }

        private int GetNum()
        {
            System.Console.WriteLine("Enter the number: ");
            int number;
            number = Convert.ToInt32(System.Console.ReadLine());

            while (number <= 0)
            {
                System.Console.WriteLine("Incorrect input. Try again: ");
                number = Convert.ToInt32(System.Console.ReadLine());
            }

            return number;
        }

        private DateTime GetOrderDate()
        {
            var dateFormat = "dd.MM.yyyy";
            System.Console.WriteLine($"Enter the order date in this format {dateFormat}:");
            bool correctDate;
            DateTime date;
            do
            {
                var dateString = System.Console.ReadLine();
                correctDate = DateTime.TryParseExact(dateString, dateFormat,
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.AllowWhiteSpaces,
                    out date);
                if (!correctDate)
                {
                    System.Console.WriteLine($"Incorrect input. Format: {dateFormat}. Try again: ");
                    continue;
                }
                if (DateTime.Compare(DateTime.Today, date) > 0)
                {
                    System.Console.WriteLine($"Incorrect date. Try again: ");
                    continue;
                }

            } while (!correctDate || DateTime.Compare(DateTime.Today, date) > 0);
            return date;
        }

        private async System.Threading.Tasks.Task<CustomerDto> CreateCustomerAsync()
        {
            System.Console.WriteLine("Enter customer name: ");
            var name = System.Console.ReadLine();
            System.Console.WriteLine("Enter the customer's phone number: ");
            var phoneNumber = System.Console.ReadLine();
            System.Console.WriteLine("Enter customer information:");
            var info = System.Console.ReadLine();
            var customer = new CustomerDto { Name = name, PhoneNumber = phoneNumber, Info = info };
            await client.CreateCustomerAsync(customer);
            return customer;
        }

        private void GetOrderInformation()
        {
            List<OrderDto> orders = client.GetOrdersAsync().Result;
            if (orders == null)
            {
                System.Console.WriteLine("No orders.");
                return;
            }

            while (true)
            {
                PrintOrders(orders, true);
                int variant = GetVariant(orders.Count + 1);
                if (variant == (orders.Count + 1))
                {
                    return;
                }
                PrintOrder(orders[variant - 1]);
            }
        }
    }
}
