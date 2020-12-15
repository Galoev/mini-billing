using System;
namespace Billing.WebApi.Console.Models
{
    public class Component
    {
        public decimal UnitPrice { get; set; }
        public string Description { get; set; }
        public QuantityType QuantityType { get; set; }
    }
}
