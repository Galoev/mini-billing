using Billing.WebApi.Client.Models;

namespace Billing.WebApi.Console.Models
{
    public class CreateComponent
    {
        public decimal UnitPrice { get; set; }
        public string Description { get; set; }
        public QuantityType QuantityType { get; set; }
    }
}
