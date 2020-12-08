﻿using System;
using System.Collections.Generic;

namespace Billing.WebApi.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

        public decimal Price { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DeliverStatus DeliverStatus { get; set; }
        public Customer Customer { get; set; }
        public ICollection<Goods> Goods { get; set; }
    }
}
