using System;
using System.Collections.Generic;

namespace Billing.WebApi.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

        public decimal Price { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DeliveryStatus DeliverStatus { get; set; }
        public Customer Customer { get; set; }
        public ICollection<Good> Goods { get; set; }
    }
}
