using Billing.WebApi.Models;
using System;

namespace Billing.WebApi.Repositories.Models
{
    public class UnitComponentPriceLinkDbo
    {
        public Guid ComponentId { get; set; }
        public QuantityType QuantityUnit { get; set; }
        public decimal UnitPrice { get; set; }

        public virtual ComponentDbo Component { get; set; }
    }
}
