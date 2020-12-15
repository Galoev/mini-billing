using System;
using System.Collections.Generic;

namespace Billing.WebApi.Client.Models
{
    public class CreateOrderDto
    {
        public CustomerDto Customer { get; set; }
        public DateTime OrderDate { get; set; }
        public int PaymentStatus { get; set; }
        public int DeliveryStatus { get; set; }
        public ICollection<OrderGoodDto> Goods { get; set; }
    }
}
