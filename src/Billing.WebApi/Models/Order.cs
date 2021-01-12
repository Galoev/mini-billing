using System;
using System.Collections.Generic;

using Billing.WebApi.Client.Models;

namespace Billing.WebApi.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }

        public decimal Price { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }

        // возможно стоит убрать customer-а и оставить только его id
        public Customer Customer { get; set; }
        public List<OrderGood> Goods { get; set; }
    }
}
