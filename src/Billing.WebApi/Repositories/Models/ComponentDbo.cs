using System.Collections.Generic;
using System;

namespace Billing.WebApi.Repositories.Models
{
    public partial class ComponentDbo
    {
        public ComponentDbo()
        {
            ComponentGoods = new HashSet<GoodComponentLinkDbo>();
            UnitComponentPrices = new HashSet<UnitComponentPriceLinkDbo>();
        }

        public Guid Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<GoodComponentLinkDbo> ComponentGoods { get; set; }
        public virtual ICollection<UnitComponentPriceLinkDbo> UnitComponentPrices { get; set; }
    }
}
