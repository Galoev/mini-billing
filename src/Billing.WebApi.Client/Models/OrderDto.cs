using System;
using System.Collections.Generic;

namespace Billing.WebApi.Client.Models
{
    public class OrderDto
    {
        public int Id { get; set; }
        public CustomerDto Customer { get; set; }
        public DateTime Date { get; set; }
        public List<Tuple<ProductDto, int>> Products { get; set; }
        public int PaymentStatus { get; set; }
        public int DeliverStatus { get; set; }
    }
}
