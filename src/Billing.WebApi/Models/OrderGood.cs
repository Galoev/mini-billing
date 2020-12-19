using System;
using Billing.WebApi.Client.Models;

namespace Billing.WebApi.Models
{
    public class OrderGood
    {
        public Guid Id { get; set; }
        public QuantityType QuantityType{ get; set; }
        public int Quantity { get; set; }
    }
}
