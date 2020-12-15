using System;

namespace Billing.WebApi.Models
{
    public class OrderGood
    {
        public Guid Id { get; set; }
        public decimal UnitPrice { get; set; }
        public QuantityType QuantityUnit { get; set; }
        public int Quantity { get; set; }
    }
}

