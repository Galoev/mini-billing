using Billing.WebApi.Client.Models;
using System;

namespace Billing.WebApi.Console.Models
{
    public class GoodInfo
    {
        public Guid Id { get; set; }
        public decimal UnitPrice { get; set; }
        public QuantityType QuantityType { get; set; }
        public string Description { get; set; }
    }
}
