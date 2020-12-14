namespace Billing.WebApi.Models
{
    public class GoodComponent
    {
        public int Id { get; set; }
        public decimal PriceForUnit { get; set; }
        public string Description { get; set; }
        public QuantityType QuantityType { get; set; }

        public int Quantity { get; set; }
    }
}
