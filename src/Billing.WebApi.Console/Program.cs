using System;
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
                menu.Print();
                int choose = console.ReadInt("Choose an option:", min: 1, max: menu.OptionsCount());
                menu.ExecuteOption(choose - 1);
            }
        }

        static void InitMainMenu()
        {
            menu = new Menu();
            menu.Add("Display all orders", DisplayOrders);
            menu.Add("Create order", CreateOrder);
            menu.Add("Exit", Exit);
        }

        static void DisplayOrders()
        {
            console.PrintTable(searchBilling.GetInfoOrders().Result);
        }

        static void CreateOrder()
        {
            List<Customer> customers = searchBilling.GetCustomers().Result;
            Customer customer = null;

            console.PrintTable(customers);
            var customerMenu = new Menu();
            customerMenu.Add("Chose customer", () => customer = customers[console.ReadInt("Enter customer number:", min: 1, max: customers.Count) - 1]);
            customerMenu.Add("Create customer", () => customer = CreateCustomer());
            customerMenu.Print();
            int choose = console.ReadInt("Choose an option:", min: 1, max: customerMenu.OptionsCount());
            customerMenu.ExecuteOption(choose - 1);

            var goodsReady = false;
            List<InfoGood> infoGoods = searchBilling.GetInfoGoods().Result;
            List<OrderGood> orderGoods = new List<OrderGood>();
            while (!goodsReady)
            {
                OrderGood orderGood = null;
                int quantity = -1;
                console.PrintTable(infoGoods);
                var goodsMenu = new Menu();
                goodsMenu.Add("Choose good", () => {
                    orderGood = new OrderGood
                    {
                        Id = infoGoods[console.ReadInt("Enter good number:", min: 1, max: infoGoods.Count) - 1].Id
                    }; 
                    quantity = console.ReadInt("Enter the quantity of goods", min: 1, max: int.MaxValue);
                    orderGood.Quantity = quantity;
                    orderGoods.Add(orderGood);
                });
                goodsMenu.Add("Done", () => goodsReady = true);
                goodsMenu.Print();

                choose = console.ReadInt("Choose an option:", min: 1, max: goodsMenu.OptionsCount());
                goodsMenu.ExecuteOption(choose - 1);
            }

            System.Console.WriteLine("Enter the order date:");
            DateTime date = console.ReadDate();

            CreateOrder order = new CreateOrder
            {
                CustomerId = customer.Id,
                CreationDate = date,
                Goods = orderGoods,
                PaymentStatus = PaymentStatus.Unpaid,
                DeliveryStatus = DeliveryStatus.DeliveryWaiting
            };

            var createdOrder = editBilling.CreateOrder(order).Result;
            System.Console.WriteLine("Order successfully created");
        }

        static void Exit()
        {
            endProgram = true;
        }

        static Customer CreateCustomer()
        {
            System.Console.WriteLine("Enter customer name: ");
            var name = System.Console.ReadLine();
            System.Console.WriteLine("Enter the customer's phone number: ");
            var phone = System.Console.ReadLine();
            System.Console.WriteLine("Enter customer information:");
            var info = System.Console.ReadLine();
            var customer = new Customer { Name = name, Phone = phone, AdditionalInfo = info };

            customer = editBilling.CreateCustomer(customer).Result;

            return customer;
        }
    }
}
