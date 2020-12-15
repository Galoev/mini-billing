using System;
using System.Collections.Generic;

namespace Billing.WebApi.Console.Models
{
    public class Good
    {
        public decimal UnitPrice { get; set; }
        public QuantityType QuantityType { get; set; }
        public string Description { get; set; }

        public ICollection<Component> Components { get; set; }
    }
}
