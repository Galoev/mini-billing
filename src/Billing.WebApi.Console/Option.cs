using System;
namespace Billing.WebApi.Console
{
    public class Option
    {
        public string Name { get; }
        public Action Callback { get; }

        public Option(string name, Action callback)
        {
            Name = name;
            Callback = callback;
        }
    }
}
