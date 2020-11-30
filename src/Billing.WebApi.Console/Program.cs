using System;

namespace Billing.WebApi.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new Client.SimpleClient("https://localhost:44311");
            System.Console.WriteLine("Enter your name:");
            var name = System.Console.ReadLine();
            try
            {
                var message = client.GetMessageAsync(name).Result;
                System.Console.WriteLine(message.message);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }

            System.Console.ReadLine();
        }
    }
}
