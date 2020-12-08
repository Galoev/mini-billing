using System.Collections.Generic;

#nullable disable

namespace Billing.WebApi.DAL.Models
{
    public partial class ComponentEntity
    {
        public ComponentEntity()
        {
            GoodsComponents = new HashSet<GoodsComponent>();
        }

        public int Id { get; set; }
        public decimal Price { get; set; }
        public int QuantityUnit { get; set; }
        public string Description { get; set; }

        public virtual ICollection<GoodsComponent> GoodsComponents { get; set; }
    }
}
