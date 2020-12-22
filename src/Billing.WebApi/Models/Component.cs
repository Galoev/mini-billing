using Billing.WebApi.Client.Models;
using System;

namespace Billing.WebApi.Models
{
    public class Component
    {
        public Guid Id { get; set; }
        public QuantityType QuantityType { get; set; }
        public string Description { get; set; }
    }
}
