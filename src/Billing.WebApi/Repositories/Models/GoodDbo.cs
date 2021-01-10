using System;
using System.Collections.Generic;
using Billing.WebApi.Client.Models;

namespace Billing.WebApi.Repositories.Models
{
    public partial class GoodDbo
    {
        public Guid Id { get; set; }
        public QuantityType QuantityType { get; set; }
        public decimal UnitPrice { get; set; }
        public string Description { get; set; }

        public virtual List<GoodComponentLinkDbo> GoodComponents { get; set; }
    }
}
