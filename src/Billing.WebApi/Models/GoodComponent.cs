using System;

namespace Billing.WebApi.Models
{
    public class Component
    {
        public Guid Id { get; set; }
        public decimal UnitPrice { get; set; }
        public string Description { get; set; }
        public QuantityType QuantityType { get; set; }
    }
}
