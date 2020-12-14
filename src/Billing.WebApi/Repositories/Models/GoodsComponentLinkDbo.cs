using System;
using System.Collections.Generic;

#nullable disable

namespace Billing.WebApi.Repositories.Models
{
    public partial class GoodsComponentLinkDbo
    {
        public int GoodsId { get; set; }
        public int ComponentId { get; set; }
        public int Quantity { get; set; }

        public virtual ComponentDbo Component { get; set; }
        public virtual GoodsDbo Goods { get; set; }
    }
}
