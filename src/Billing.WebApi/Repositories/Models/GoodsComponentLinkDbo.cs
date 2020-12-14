using Billing.WebApi.Models;
using System;

#nullable disable

namespace Billing.WebApi.Repositories.Models
{
    public partial class GoodsComponentLinkDbo
    {
        public Guid GoodsId { get; set; }
        public Guid ComponentId { get; set; }
        public int Quantity { get; set; }

        public QuantityType QuantityUnit { get; set; }

        public virtual ComponentDbo Component { get; set; }
        public virtual GoodsDbo Goods { get; set; }
    }
}
