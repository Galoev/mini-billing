using System;

namespace Billing.WebApi.Repositories.Models
{
    public partial class GoodComponentLinkDbo
    {
        public Guid GoodId { get; set; }
        public Guid ComponentId { get; set; }
        public int Quantity { get; set; }
    }
}
