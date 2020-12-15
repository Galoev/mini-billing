using Billing.WebApi.Models;
using System;


namespace Billing.WebApi.Repositories.Models
{
    public partial class OrderGoodLinkDbo
    {
        public Guid OrderId { get; set; }
        public Guid GoodId { get; set; }
        public int Quantity { get; set; }
        public QuantityType QuantityUnit { get; set; }

        public virtual GoodDbo Good { get; set; }
        public virtual OrderDbo Order { get; set; }
    }
}
