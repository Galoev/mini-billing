using System;
using System.Collections.Generic;

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
            throw new NotImplementedException();
        }

        public DateTime ReadDate()
        {
            throw new NotImplementedException();
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
    }
}
