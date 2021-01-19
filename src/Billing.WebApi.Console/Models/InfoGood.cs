using Billing.WebApi.Client.Models;

namespace Billing.WebApi.Console.Models
{
    public class InfoGood
    {
        public decimal UnitPrice { get; set; }
        public QuantityType QuantityType { get; set; }
        public string Description { get; set; }
    }
}
