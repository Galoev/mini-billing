using System.Collections.Generic;
using System;

namespace Billing.WebApi.Models
{
    public class Good
    {
        public Guid Id { get; set; }
        public string Description { get; set; }

        public List<GoodComponent> Components { get; set; }
    }
}
