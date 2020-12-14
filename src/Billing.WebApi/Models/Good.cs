using System.Collections.Generic;

namespace Billing.WebApi.Models
{
    public class Good
    {
        public int Id { get; set; }
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public QuantityType QuantityType { get; set; }
        public string Description { get; set; }

        public ICollection<GoodComponent> Components { get; set; }
    }
}
