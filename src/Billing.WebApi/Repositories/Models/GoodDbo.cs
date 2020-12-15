using System.Collections.Generic;
using System;


namespace Billing.WebApi.Repositories.Models
{
    public partial class GoodDbo
    {
        public GoodDbo()
        {
            GoodComponents = new HashSet<GoodComponentLinkDbo>();
            GoodOrders = new HashSet<OrderGoodLinkDbo>();
            UnitGoodPrices = new HashSet<UnitGoodPriceLinkDbo>();
        }

        public Guid Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<GoodComponentLinkDbo> GoodComponents { get; set; }
        public virtual ICollection<OrderGoodLinkDbo> GoodOrders { get; set; }
        public virtual ICollection<UnitGoodPriceLinkDbo> UnitGoodPrices { get; set; }
    }
}
