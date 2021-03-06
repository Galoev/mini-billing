﻿using System.Collections.Generic;

namespace Billing.WebApi.Client.Models
{
    public class CreateGoodDto
    {
        public QuantityType QuantityType { get; set; }
        public decimal UnitPrice { get; set; }
        public string Description { get; set; }

        public List<GoodComponentDto> Components { get; set; }
    }
}
