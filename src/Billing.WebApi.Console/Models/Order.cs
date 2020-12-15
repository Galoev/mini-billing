using System;
using System.Collections.Generic;

namespace Billing.WebApi.Console.Models
{
    public class Order
    {
        public Customer Customer { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal Price { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
        public ICollection<OrderGood> Goods { get; set; }
    }
}
