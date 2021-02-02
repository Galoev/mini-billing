using System.Collections.Generic;
using Billing.WebApi.Client.Models;
using Billing.WebApi.Console.Models;

namespace Billing.WebApi.Console
{
    class Program
    {
        private static Menu menu;
        private static Console console;
        private static SearchBilling searchBilling;
        private static EditBilling editBilling;
        private static bool endProgram = false;

        static void Main(string[] args)
        {
            editBilling = new EditBilling();
            searchBilling = new SearchBilling();            
            console = new Console();
            InitMainMenu();
            while (!endProgram)
            {
                console.PrintMenu(menu);
                int choose = console.ReadNumberWithHint("Choose an option:", 
                    minBound: 1, maxBound: menu.Options.Count);
                menu.ExecuteOption(choose - 1);
            }
        }

        static void InitMainMenu()
        {
            menu = new Menu();
            menu.Add("Display all orders", () => 
            {
                var result = searchBilling.GetInfoOrders().Result;
                console.PrintTable(result);
            });
            menu.Add("Create order", CreateOrder);
            menu.Add("Exit", Exit);
        }

        static void CreateOrder()
        {
            List<Customer> availableCustomers = searchBilling.GetCustomers().Result;
            console.PrintTable(availableCustomers);
            Customer chosenCustomer = null;

            var customerChoosingMenu = new Menu();
            customerChoosingMenu.Add("Choose customer", () =>
            {
                var customerNumber = console.ReadNumberWithHint("Enter customer number:",
                    minBound: 1, maxBound: availableCustomers.Count);
                chosenCustomer = availableCustomers[customerNumber - 1];
            });
            customerChoosingMenu.Add("Create customer", () => chosenCustomer = CreateCustomer());

            console.PrintMenu(customerChoosingMenu);

            int chosenOption = console.ReadNumberWithHint("Choose an option: ", 
                minBound: 1, maxBound: customerChoosingMenu.Options.Count);
            customerChoosingMenu.ExecuteOption(chosenOption - 1);

            List<GoodInfo> infoGoods = searchBilling.GetInfoGoods().Result;

            CreateOrder order = console.ReadOrder(infoGoods);
            order.CustomerId = chosenCustomer.Id;
            order.PaymentStatus = PaymentStatus.Unpaid;
            order.DeliveryStatus = DeliveryStatus.DeliveryWaiting;
            
            var createdOrder = editBilling.CreateOrder(order).Result;
            console.PrintInfoMessage("Order successfully created!");
        }

        static Customer CreateCustomer()
        {
            var customer = console.ReadCustomer();
            customer = editBilling.CreateCustomer(customer).Result;
            return customer;
        }

        static void Exit()
        {
            endProgram = true;
        }
    }
}
