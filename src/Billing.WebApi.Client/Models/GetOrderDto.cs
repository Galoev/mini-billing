using System;
using System.Collections.Generic;

namespace Billing.WebApi.Client.Models
{
    public class GetOrderDto
    {
        public Guid Id { get; set; }
        public CustomerDto Customer { get; set; }
        public DateTime OrderDate { get; set; }

        public decimal Price { get; set; }
        public int PaymentStatus { get; set; }
        public int DeliveryStatus { get; set; }
        public ICollection<OrderGoodDto> Goods { get; set; }
    }
}
