using System.Collections.Generic;

namespace Billing.WebApi.Models
{
    public class Goods
    {
        public int Id { get; set; }
        public decimal Price { get; set; }

        public int QuantityInOrder { get; set; }

        public Quantity QuantityUnit { get; set; }
        public string Description { get; set; }

        public ICollection<Component> Components { get; set; }
    }
}
