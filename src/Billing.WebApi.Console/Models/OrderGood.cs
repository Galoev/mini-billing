using System;
namespace Billing.WebApi.Console.Models
{
    public class OrderGood
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int QuantityUnit { get; set; }
    }
}
