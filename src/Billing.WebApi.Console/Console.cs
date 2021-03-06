﻿using System;
using System.Collections.Generic;
using System.Linq;
using BetterConsoleTables;
using Billing.WebApi.Client.Models;
using Billing.WebApi.Console.Models;
using Billing.WebApi.Console.Validators;

namespace Billing.WebApi.Console
{
    public class Console : IConsole
    {
        public Console()
        {
        }

        public void PrintTable<T>(IEnumerable<T> values)
        {
            Table table = new Table();
            var columns = GetColumns<T>();
            var columnsWithIdx = columns.ToList();
            columnsWithIdx.Insert(0, "№");
            table.AddColumns(Alignment.Left, Alignment.Left, columnsWithIdx.ToArray());
            int index = 0;
            foreach (
                var propertyValues
                in values.Select(value => columns.Select(column => GetColumnValue<T>(value, column)))
            )
            {
                index++;
                var row = propertyValues.ToList();
                row.Insert(0, index);
                table.AddRow(row.ToArray());
            }
            if (table.Rows.Count > 0)
            {
                System.Console.Write(table.ToString());
                System.Console.WriteLine();
            } else
            {
                PrintInfoMessage("Table is empty!");
                System.Console.WriteLine();
            }
        }

        public void PrintMenu(Menu menu)
        {
            for (int i = 0; i < menu.Options.Count; i++)
            {
                System.Console.WriteLine($"{i + 1}. {menu.Options[i].Name}");
            }
            System.Console.WriteLine();
        }

        public void PrintErrorMessage(string errorMessage)
        {
            System.Console.WriteLine($"An error occured: {errorMessage}");
        }

        public void PrintInfoMessage(string message)
        {
            System.Console.WriteLine(message);
        }

        public DateTime ReadDateWithHint(string hint)
        {
            System.Console.WriteLine(hint);

            string dateFormat = "dd.MM.yyyy";
            string input = System.Console.ReadLine();
            DateTime date;

            while (!UserInputValidator.IsValidDateInFormat(input, dateFormat, out date))
            {
                System.Console.WriteLine($"Please enter the order date in this format {dateFormat}!");
                System.Console.WriteLine(hint);
                input = System.Console.ReadLine();
            }
            System.Console.WriteLine();

            return date;
        }

        public decimal ReadPriceWithHint(string hint)
        {
            System.Console.WriteLine(hint);

            string input = System.Console.ReadLine();
            decimal price;

            while (!UserInputValidator.IsValidPrice(input, out price))
            {
                System.Console.WriteLine($"Please enter a non-negative decimal!");
                System.Console.WriteLine(hint);
                input = System.Console.ReadLine();
            }
            System.Console.WriteLine();

            return price;
        }

        public int ReadNumberWithHint(string hint, int minBound, int maxBound)
        {
            System.Console.WriteLine(hint);

            var input = System.Console.ReadLine();
            int number;

            while (!UserInputValidator.IsValidNumber(input, minBound, maxBound, out number))
            {
                System.Console.WriteLine($"Please enter a number between {minBound} and {maxBound}!");
                System.Console.WriteLine(hint);
                input = System.Console.ReadLine();
            }
            System.Console.WriteLine();

            return number;
        }

        public string ReadLineWithHint(string hint, bool isRequired = false)
        {
            System.Console.WriteLine(hint);
            var input = System.Console.ReadLine();

            while (isRequired && string.IsNullOrWhiteSpace(input))
            {
                System.Console.WriteLine($"Please enter non-empty string!");
                input = System.Console.ReadLine();
            }

            return string.IsNullOrWhiteSpace(input) ? string.Empty : input;
        }

        public string ReadPhoneNumberWithHint(string hint)
        {
            System.Console.WriteLine(hint);
            var format = "[+7|7|8]([ddd|dddd])ddd-dd-dd";
            var phone = ReadLineWithHint($"Enter the phone number in this format {format}: ");

            while (!UserInputValidator.IsValidPhoneNumber(phone))
            {
                phone = ReadLineWithHint($"Please enter a valid phone number in this format {format}!");
            }
            System.Console.WriteLine();

            return phone;
        }

        public Customer ReadCustomer() => new Customer
        {
            Name = ReadLineWithHint("Enter customer name: ", isRequired: true),
            Phone = ReadPhoneNumberWithHint("Enter customer phone number: "),
            AdditionalInfo = ReadLineWithHint("Enter customer information: ")
        };

        public CreateOrder ReadOrder(List<GoodInfo> goodsInfo)
        {
            var orderGoods = new List<OrderGood>();

            OrderGood chosenGood;
            int chosenGoodQuantity;

            var goodsMenu = new Menu();
            goodsMenu.Add("Choose good", () => {
                chosenGood = new OrderGood
                {
                    Id = goodsInfo[ReadNumberWithHint("Enter a good number: ",
                        minBound: 1, maxBound: goodsInfo.Count) - 1].Id
                };
                chosenGoodQuantity = ReadNumberWithHint("Enter the quantity of the good: ",
                    minBound: 1, maxBound: int.MaxValue);
                chosenGood.Quantity = chosenGoodQuantity;
                orderGoods.Add(chosenGood);
            });
            
            var goodsReady = false;
            goodsMenu.Add("Done", () => goodsReady = true);
            
            while (!goodsReady)
            {
                PrintTable(goodsInfo);
                PrintMenu(goodsMenu);

                var chosenOption = ReadNumberWithHint("Choose an option:", 
                    minBound: 1, maxBound: goodsMenu.Options.Count);
                goodsMenu.ExecuteOption(chosenOption - 1);
            }

            DateTime date = ReadDateWithHint("Enter the order date: ");

            return new CreateOrder
            {
                CreationDate = date,
                Goods = orderGoods
            };
        }

        public CreateGood ReadGood(List<Component> componentInfo)
        {
            var goodComponents = new List<GoodComponent>();

            GoodComponent chosenComponent;
            int chosenComponentQuantity;

            var componentsMenu = new Menu();
            componentsMenu.Add("Choose component", () => {
                chosenComponent = new GoodComponent
                {
                    Id = componentInfo[ReadNumberWithHint("Enter a component number: ",
                        minBound: 1, maxBound: componentInfo.Count) - 1].Id
                };
                chosenComponentQuantity = ReadNumberWithHint("Enter the quantity of the component: ",
                    minBound: 1, maxBound: int.MaxValue);
                chosenComponent.Quantity = chosenComponentQuantity;
                goodComponents.Add(chosenComponent);
            });

            var goodsReady = false;
            componentsMenu.Add("Done", () => goodsReady = true);

            while (!goodsReady)
            {
                PrintTable(componentInfo);
                PrintMenu(componentsMenu);

                var chosenOption = ReadNumberWithHint("Choose an option:",
                    minBound: 1, maxBound: componentsMenu.Options.Count);
                componentsMenu.ExecuteOption(chosenOption - 1);
            }

            decimal unitPrice = ReadPriceWithHint("Enter an unit price of the good: ");
            string description = ReadLineWithHint("Enter a description of the good: ");
            int quantityTypeNumber = ReadNumberWithHint("Enter a number of quantity type, where kilogram - 1, litre - 2, piece - 3 ", 
                1, Enum.GetNames(typeof(QuantityType)).Length);

            return new CreateGood
            {
                UnitPrice = unitPrice,
                QuantityType = (QuantityType) quantityTypeNumber,
                Components = goodComponents,
                Description = description
            };
        }

        public CreateComponent ReadComponent()
        {
            int quantityType = ReadNumberWithHint("Enter a number of quantity type, where kilogram - 1, litre - 2, piece - 3 ",
                1, Enum.GetNames(typeof(QuantityType)).Length);
            return new CreateComponent
            {
                UnitPrice = ReadPriceWithHint("Enter an unit price of the component: "),
                Description = ReadLineWithHint("Enter a description of the component: "),
                QuantityType = (QuantityType) quantityType
            };
        }

        private static IEnumerable<string> GetColumns<T>()
        {
            return typeof(T).GetProperties().Select(x => x.Name).ToArray();
        }

        private static object GetColumnValue<T>(object target, string column)
        {
            return typeof(T).GetProperty(column).GetValue(target, null);
        }
    }
}
