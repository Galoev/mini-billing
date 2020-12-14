#nullable disable

namespace Billing.WebApi.Repositories.Models
{
    public partial class OrderGoodsLinkDbo
    {
        public int OrderId { get; set; }
        public int GoodsId { get; set; }
        public int Quantity { get; set; }

        public virtual GoodsDbo Goods { get; set; }
        public virtual OrderDbo Order { get; set; }
    }
}
