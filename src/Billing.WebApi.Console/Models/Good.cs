using Billing.WebApi.Client.Models;
using System;
using System.Collections.Generic;

namespace Billing.WebApi.Console.Models
{
    public class Good
    {
        public Guid Id { get; set; }
        public decimal UnitPrice { get; set; }
        public QuantityType QuantityType { get; set; }
        public string Description { get; set; }

        public List<GoodComponent> Components { get; set; }
    }
}
