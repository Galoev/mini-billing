using System.Collections.Generic;
using System;
using Billing.WebApi.Client.Models;

namespace Billing.WebApi.Models
{
    public class Good
    {
        public Guid Id { get; set; }

        public QuantityType QuantityType { get; set; }
        public string Description { get; set; }

        public List<GoodComponent> Components { get; set; }
    }
}
