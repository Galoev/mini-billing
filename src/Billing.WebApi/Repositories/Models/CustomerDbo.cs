using System;
using System.Collections.Generic;

namespace Billing.WebApi.Repositories.Models
{
    public partial class CustomerDbo
    {
        public CustomerDbo()
        {
            Orders = new HashSet<OrderDbo>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string AdditionalInfo { get; set; }

        public virtual ICollection<OrderDbo> Orders { get; set; }
    }
}
