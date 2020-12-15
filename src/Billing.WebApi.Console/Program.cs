using System;
using System.Collections.Generic;
using Billing.WebApi.Console.Models;

namespace Billing.WebApi.Console
{
    class Program
    {
        private static Menu menu;
        private static Console console;
        private static IController controller;

        static void Main(string[] args)
        {
            controller = new Controller();
            console = new Console();
            InitMainMenu();
            menu.Print();
            int choose = console.ReadInt("Choose an option:", min: 1, max: menu.OptionsCount());
            menu.ExecuteOption(choose - 1);
        }

        static void InitMainMenu()
        {
            menu = new Menu();
            menu.Add("Display all orders", DisplayOrders);
            menu.Add("Create order", CreateOrder);
            menu.Add("Order Information", DisplayOrderInformation);
            menu.Add("Exit", Exit);
        }

        static void DisplayOrders()
        {
            console.PrintTable(controller.getOrders());
        }

        static void CreateOrder()
        {
            throw new NotImplementedException();
        }

        static void DisplayOrderInformation()
        {
            throw new NotImplementedException();
        }

        static void Exit()
        {
            throw new NotImplementedException();
        }
    }
}
