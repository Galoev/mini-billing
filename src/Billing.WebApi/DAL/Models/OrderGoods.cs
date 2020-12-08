#nullable disable

namespace Billing.WebApi.DAL.Models
{
    public partial class OrderGoods
    {
        public int OrderId { get; set; }
        public int GoodsId { get; set; }
        public int Quantity { get; set; }

        public virtual GoodsEntity Goods { get; set; }
        public virtual OrderEntity Order { get; set; }
    }
}
