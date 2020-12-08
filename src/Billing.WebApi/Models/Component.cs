namespace Billing.WebApi.Models
{
    public class Component
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public Quantity QuantityUnit { get; set; }

        public int QuantityInGoods { get; set; }
    }
}
