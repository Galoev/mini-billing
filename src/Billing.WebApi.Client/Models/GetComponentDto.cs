﻿using System;

namespace Billing.WebApi.Client.Models
{
    public class GetComponentDto
    {
        public Guid Id { get; set; }
        public decimal UnitPrice { get; set; }
        public QuantityType QuantityType { get; set; }
        public string Description { get; set; }
    }
}
