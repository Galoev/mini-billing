using System;
using System.Collections.Generic;
using Billing.WebApi.Client.Models;

namespace Billing.WebApi.Repositories.Models
{
    public partial class OrderDbo
    {
        public OrderDbo()
        {
            OrderGoods = new List<OrderGoodLinkDbo>();
        }
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime CreationDate { get; set; }
        public decimal Price { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }

        public virtual List<OrderGoodLinkDbo> OrderGoods { get; set; }
    }
}
