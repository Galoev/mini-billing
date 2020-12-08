using System.Collections.Generic;

#nullable disable

namespace Billing.WebApi.DAL.Models
{
    public partial class GoodsEntity
    {
        public GoodsEntity()
        {
            GoodsComponents = new HashSet<GoodsComponent>();
            OrderGoods = new HashSet<OrderGoods>();
        }

        public int Id { get; set; }
        public decimal Price { get; set; }
        public int QuantityUnit { get; set; }
        public string Description { get; set; }

        public virtual ICollection<GoodsComponent> GoodsComponents { get; set; }
        public virtual ICollection<OrderGoods> OrderGoods { get; set; }
    }
}
