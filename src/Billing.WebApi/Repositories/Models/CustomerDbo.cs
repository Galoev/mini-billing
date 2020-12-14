using System;
using System.Collections.Generic;

#nullable disable

namespace Billing.WebApi.Repositories.Models
{
    public partial class CustomerDbo
    {
        public CustomerDbo()
        {
            Orders = new HashSet<OrderDbo>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string AdditionalInfo { get; set; }

        public virtual ICollection<OrderDbo> Orders { get; set; }
    }
}
