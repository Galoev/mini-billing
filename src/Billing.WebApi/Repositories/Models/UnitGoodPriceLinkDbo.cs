using Billing.WebApi.Models;
using System;

namespace Billing.WebApi.Repositories.Models
{
    public class UnitGoodPriceLinkDbo
    {
        public Guid GoodId { get; set; }
        public QuantityType QuantityUnit { get; set; }
        public decimal UnitPrice { get; set; }

        public virtual GoodDbo Good { get; set; }
    }
}
