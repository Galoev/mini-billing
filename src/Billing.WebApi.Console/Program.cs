using System.Collections.Generic;
using Billing.WebApi.Client.Models;
using Billing.WebApi.Console.Models;

namespace Billing.WebApi.Console
{
    class Program
    {
        private static Menu menu;
        private static IConsole console;
        private static ISearchBilling searchBilling;
        private static IEditBilling editBilling;
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
                var result = searchBilling.GetOrdersInfo().Result;
                if (result.IsSuccess)
                    console.PrintTable(result.Value);
                else
                    console.PrintErrorMessage(result.Message);
            });
            menu.Add("Create order", CreateOrder);
            menu.Add("Create good", CreateGood);
            menu.Add("Create component", CreateComponent);
            menu.Add("Exit", Exit);
        }

        static void CreateOrder()
        {
            var resultWithListOfCustomers = searchBilling.GetCustomers().Result;
            List<Customer> availableCustomers;
            if (resultWithListOfCustomers.IsSuccess)
            {
                availableCustomers = resultWithListOfCustomers.Value;
            } else
            {
                console.PrintErrorMessage(resultWithListOfCustomers.Message);
                return;
            }
                
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

            bool canceled = false;
            customerChoosingMenu.Add("Cancel", () => canceled = true);

            console.PrintMenu(customerChoosingMenu);

            int chosenOption;
            while (chosenCustomer is null)
            {
                chosenOption = console.ReadNumberWithHint("Choose an option: ",
                    minBound: 1, maxBound: customerChoosingMenu.Options.Count);
                customerChoosingMenu.ExecuteOption(chosenOption - 1);
                if (canceled)
                    return;
            }

            var resultWithGoodsInfo = searchBilling.GetGoodsInfo().Result;
            List<GoodInfo> goodsInfo;
            if (resultWithGoodsInfo.IsSuccess)
            {
                goodsInfo = resultWithGoodsInfo.Value;
            } else
            {
                console.PrintErrorMessage(resultWithGoodsInfo.Message);
                return;
            }

            CreateOrder order = console.ReadOrder(goodsInfo);
            order.CustomerId = chosenCustomer.Id;
            order.PaymentStatus = PaymentStatus.Unpaid;
            order.DeliveryStatus = DeliveryStatus.DeliveryWaiting;
            
            var resultWithCreatedOrder = editBilling.CreateOrder(order).Result;
            if (resultWithCreatedOrder.IsSuccess)
                console.PrintInfoMessage(resultWithCreatedOrder.Message);
            else
                console.PrintErrorMessage(resultWithCreatedOrder.Message);
        }

        static void CreateGood()
        {
            var resultWithComponentsInfo = searchBilling.GetComponents().Result;
            List<Component> componentsInfo;
            if (resultWithComponentsInfo.IsSuccess)
            {
                componentsInfo = resultWithComponentsInfo.Value;
            }
            else
            {
                console.PrintErrorMessage(resultWithComponentsInfo.Message);
                return;
            }

            CreateGood good = console.ReadGood(componentsInfo);
            
            var resultWithCreatedOrder = editBilling.CreateGood(good).Result;
            if (resultWithCreatedOrder.IsSuccess)
                console.PrintInfoMessage(resultWithCreatedOrder.Message);
            else
                console.PrintErrorMessage(resultWithCreatedOrder.Message);
        }

        private static Customer CreateCustomer()
        {
            var customer = console.ReadCustomer();
            var resultWithCreatedCustomer = editBilling.CreateCustomer(customer).Result;
            if (resultWithCreatedCustomer.IsSuccess)
            {
                customer = resultWithCreatedCustomer.Value;
                console.PrintInfoMessage(resultWithCreatedCustomer.Message);
            } else
            {
                console.PrintErrorMessage(resultWithCreatedCustomer.Message);
                customer = null;
            }
            return customer;
        }

        static void CreateComponent()
        {
            var componentInfo = console.ReadComponent();
            var resultWithCreatedComponent = editBilling.CreateComponent(componentInfo).Result;
            if (resultWithCreatedComponent.IsSuccess)
                console.PrintInfoMessage(resultWithCreatedComponent.Message);
            else
                console.PrintErrorMessage(resultWithCreatedComponent.Message);
        }

        static void Exit()
        {
            endProgram = true;
        }
    }
}
