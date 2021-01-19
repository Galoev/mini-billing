using Billing.WebApi.Client.Models;
using System;

namespace Billing.WebApi.Console.Models
{
    public class InfoOrder
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime CreationDate { get; set; }
        public decimal Price { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
    }
}
