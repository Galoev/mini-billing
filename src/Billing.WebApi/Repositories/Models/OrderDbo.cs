using System;

using Billing.WebApi.Client.Models;

namespace Billing.WebApi.Repositories.Models
{
    public partial class OrderDbo
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime CreationDate { get; set; }
        public decimal Price { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
    }
}
