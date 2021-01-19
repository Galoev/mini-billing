using Billing.WebApi.Client.Models;
using System;

namespace Billing.WebApi.Console.Models
{
    public class Component
    {
        public Guid Id { get; set; }
        public decimal UnitPrice { get; set; }
        public string Description { get; set; }
        public QuantityType QuantityType { get; set; }
    }
}
