using System;
using System.Collections.Generic;

namespace Billing.WebApi.Console
{
    public class Menu
    {
        private IList<Option> options;

        public Menu()
        {
            options = new List<Option>();
        }

        public void Print()
        {
            for (int i = 0; i < options.Count; i++)
            {
                System.Console.WriteLine($"{i + 1}. {options[i].name}");
            }
        }

        public Menu Add(string option, Action callback)
        {

            return Add(new Option(option, callback));
        }

        public Menu Add(Option option)
        {
            options.Add(option);
            return this;
        }

        public void ExecuteOption(int i)
        {
            options[i].callback();
        }

        public int OptionsCount()
        {
            return options.Count;
        }
    }
}
