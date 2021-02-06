﻿using Billing.WebApi.Client.Models;
using System;
using System.Collections.Generic;

namespace Billing.WebApi.Console.Models
{
    public class CreateOrder
    {
        public Guid CustomerId { get; set; }
        public DateTime CreationDate { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
        public List<OrderGood> Goods { get; set; }
    }
}
