using System;
using System.Collections.Generic;

#nullable disable

namespace Billing.WebApi.DAL.Models
{
    public partial class GoodsComponent
    {
        public int GoodsId { get; set; }
        public int ComponentId { get; set; }
        public int Quantity { get; set; }

        public virtual ComponentEntity Component { get; set; }
        public virtual GoodsEntity Goods { get; set; }
    }
}
