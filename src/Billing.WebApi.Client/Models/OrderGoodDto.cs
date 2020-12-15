using System;

namespace Billing.WebApi.Client.Models
{
    public class OrderGoodDto
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int QuantityUnit { get; set; }
    }
}
