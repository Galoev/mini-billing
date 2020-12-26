namespace Billing.WebApi.Client.Models
{
    public class CreateComponentDto
    {
        public decimal UnitPrice { get; set; }
        public QuantityType QuantityType { get; set; }
        public string Description { get; set; }
    }
}
