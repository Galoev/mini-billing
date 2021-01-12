using System;
using System.Collections.Generic;
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
            console.PrintTable(searchBilling.GetInfoOrders());
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
            List<OrderGood> goods = searchBilling.GetOrderGoods();
            List<InfoGood> infoGoods = searchBilling.GetInfoGoods();
            List<OrderGood> orderGoods = new List<OrderGood>();
            while (!goodsReady)
            {
                OrderGood orderGood = null;
                int quantity = -1;
                console.PrintTable(infoGoods);
                var goodsMenu = new Menu();
                goodsMenu.Add("Chose good", () => {
                    orderGood = goods[console.ReadInt("Enter good number:", min: 1, max: goods.Count) - 1];
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
                Customer = customer,
                OrderDate = date,
                Goods = orderGoods,
                PaymentStatus = PaymentStatus.Unpaid,
                DeliveryStatus = DeliveryStatus.DeliveryWaiting
            };

            editBilling.CreateOrder(order);
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
            return customer;
        }
    }
}
