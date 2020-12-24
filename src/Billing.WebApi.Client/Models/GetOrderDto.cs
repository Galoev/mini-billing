using System;
using System.Collections.Generic;

namespace Billing.WebApi.Client.Models
{
    public class GetOrderDto
    {
        public Guid Id { get; set; }
        public CustomerDto Customer { get; set; }
        public DateTime CreationDate { get; set; }

        public decimal Price { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
        public List<OrderGoodDto> Goods { get; set; }
    }
}
