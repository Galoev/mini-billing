using Billing.WebApi.Models;
using System.Collections.Generic;
using System;

#nullable disable

namespace Billing.WebApi.Repositories.Models
{
    public partial class ComponentDbo
    {
        public ComponentDbo()
        {
            GoodsComponents = new HashSet<GoodsComponentLinkDbo>();
        }

        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public virtual ICollection<GoodsComponentLinkDbo> GoodsComponents { get; set; }
    }
}
