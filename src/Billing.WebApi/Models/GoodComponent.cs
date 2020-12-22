using System;

namespace Billing.WebApi.Models
{
    public class GoodComponent
    {
        public Guid Id { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
