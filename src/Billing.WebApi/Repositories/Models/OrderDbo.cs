using System;
using System.Collections.Generic;

#nullable disable

namespace Billing.WebApi.Repositories.Models
{
    public partial class OrderDbo
    {
        public OrderDbo()
        {
            OrderGoods = new HashSet<OrderGoodsLinkDbo>();
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Price { get; set; }
        public int PaymentStatus { get; set; }
        public int DeliverStatus { get; set; }

        public virtual CustomerDbo Customer { get; set; }
        public virtual ICollection<OrderGoodsLinkDbo> OrderGoods { get; set; }
    }
}
