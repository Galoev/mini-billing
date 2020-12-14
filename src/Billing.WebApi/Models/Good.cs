using System.Collections.Generic;
using System;

namespace Billing.WebApi.Models
{
    public class Good
    {
        public Guid Id { get; set; }
        public decimal UnitPrice { get; set; }
        public QuantityType QuantityType { get; set; }
        public string Description { get; set; }

        public ICollection<GoodComponent> Components { get; set; }
    }
}
