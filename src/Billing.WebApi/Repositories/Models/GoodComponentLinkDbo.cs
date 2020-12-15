using Billing.WebApi.Models;
using System;


namespace Billing.WebApi.Repositories.Models
{
    public partial class GoodComponentLinkDbo
    {
        public Guid GoodId { get; set; }
        public Guid ComponentId { get; set; }
        public int Quantity { get; set; }
        public QuantityType QuantityUnit { get; set; }

        public virtual ComponentDbo Component { get; set; }
        public virtual GoodDbo Good { get; set; }
    }
}
