using System;
using System.Collections.Generic;

namespace Billing.WebApi.Console.Models
{
    public class CreateOrder
    {
        public Customer Customer { get; set; }
        public DateTime OrderDate { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
        public List<OrderGood> Goods { get; set; }
    }
}
