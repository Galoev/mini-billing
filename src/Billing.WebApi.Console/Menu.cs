using System;
using System.Collections.Generic;

namespace Billing.WebApi.Console
{
    public class Menu
    {
        public IList<Option> Options { get; }

        public Menu()
        {
            Options = new List<Option>();
        }

        public Menu Add(string option, Action callback)
        {

            return Add(new Option(option, callback));
        }

        public Menu Add(Option option)
        {
            Options.Add(option);
            return this;
        }

        public void ExecuteOption(int i)
        {
            Options[i].Callback();
        }
    }
}
