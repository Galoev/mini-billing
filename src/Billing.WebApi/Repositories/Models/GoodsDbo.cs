using System.Collections.Generic;
using System;


namespace Billing.WebApi.Repositories.Models
{
    public partial class GoodsDbo
    {
        public GoodsDbo()
        {
            GoodsComponents = new HashSet<GoodsComponentLinkDbo>();
            OrderGoods = new HashSet<OrderGoodsLinkDbo>();
        }

        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public virtual ICollection<GoodsComponentLinkDbo> GoodsComponents { get; set; }
        public virtual ICollection<OrderGoodsLinkDbo> OrderGoods { get; set; }
    }
}
