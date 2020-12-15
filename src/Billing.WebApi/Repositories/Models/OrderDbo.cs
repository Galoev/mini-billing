using System;
using System.Collections.Generic;

using Billing.WebApi.Models;

namespace Billing.WebApi.Repositories.Models
{
    public partial class OrderDbo
    {
        public OrderDbo()
        {
            OrderGoods = new HashSet<OrderGoodLinkDbo>();
        }

        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Price { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DeliveryStatus DeliverStatus { get; set; }

        public virtual CustomerDbo Customer { get; set; }
        public virtual ICollection<OrderGoodLinkDbo> OrderGoods { get; set; }
    }
}
