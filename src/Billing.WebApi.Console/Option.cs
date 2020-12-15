using System;
namespace Billing.WebApi.Console
{
    public class Option
    {
        public string name;
        public Action callback;

        public Option(string name, Action callback)
        {
            this.name = name;
            this.callback = callback;
        }
    }
}
