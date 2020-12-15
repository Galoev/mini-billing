using System;
using System.Collections.Generic;
using System.Linq;
using BetterConsoleTables;

namespace Billing.WebApi.Console
{
    public class Console: IConsole
    {
        public Console()
        {
        }

        public void AddMenu(Menu menu)
        {
            throw new NotImplementedException();
        }

        public void PrintMenu()
        {
            throw new NotImplementedException();
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

            System.Console.Write(table.ToString());
        }

        public DateTime ReadDate()
        {
            string dateFormat = "dd.MM.yyyy";
            string input = System.Console.ReadLine();
            DateTime date;

            while (!DateTime.TryParseExact(input, dateFormat,
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.AllowWhiteSpaces,
                    out date))
            {
                System.Console.WriteLine($"Please enter the order date in this format {dateFormat}");
                input = System.Console.ReadLine();
            }

            return date;
        }

        public int ReadInt(string hint, int min, int max)
        {
            System.Console.WriteLine(hint);
            return ReadInt(min, max);
        }

        public int ReadInt(int min, int max)
        {
            int value = ReadInt();

            while (value < min || value > max)
            {
                System.Console.WriteLine($"Please enter a number between {min} and {max}");
                value = ReadInt();
            }

            return value;
        }

        public int ReadInt()
        {
            string input = System.Console.ReadLine();
            int value;

            while (!int.TryParse(input, out value))
            {
                System.Console.WriteLine($"Please enter a number");
                input = System.Console.ReadLine();
            }

            return value;
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
