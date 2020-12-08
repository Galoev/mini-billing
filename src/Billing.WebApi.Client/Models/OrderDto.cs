using System;

namespace Billing.WebApi.Client.Models
{
    public class OrderDto
    {
        public CustomerDto Customer { get; set; }

        public DateTime PaymentDate { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal Price { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DeliverStatus DeliverStatus { get; set; }
    }
}
