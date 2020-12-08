using System;
using System.Collections.Generic;

#nullable disable

namespace Billing.WebApi.DAL.Models
{
    public partial class CustomerEntity
    {
        public CustomerEntity()
        {
            Orders = new HashSet<OrderEntity>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string AdditionalInfo { get; set; }

        public virtual ICollection<OrderEntity> Orders { get; set; }
    }
}
