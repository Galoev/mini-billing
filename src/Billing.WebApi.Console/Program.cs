using System;
using System.Collections.Generic;
using Billing.WebApi.Client.Models;

namespace Billing.WebApi.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var console = new Console("https://localhost:5001");
            console.Run();
        }
    }
}
