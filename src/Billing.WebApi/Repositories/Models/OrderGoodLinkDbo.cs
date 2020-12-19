using System;


namespace Billing.WebApi.Repositories.Models
{
    public partial class OrderGoodLinkDbo
    {
        public Guid OrderId { get; set; }
        public Guid GoodId { get; set; }
        public int Quantity { get; set; }
    }
}
