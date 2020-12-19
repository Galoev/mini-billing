using System;
using Billing.WebApi.Client.Models;

namespace Billing.WebApi.Repositories.Models
{
    public partial class GoodDbo
    {
        public Guid Id { get; set; }
        public QuantityType QuantityType { get; set; }
        public decimal UnitPrice { get; set; }
        public string Description { get; set; }
    }
}
