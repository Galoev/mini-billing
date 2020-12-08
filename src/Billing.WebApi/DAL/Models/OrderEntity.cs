using System;
using System.Collections.Generic;

#nullable disable

namespace Billing.WebApi.DAL.Models
{
    public partial class OrderEntity
    {
        public OrderEntity()
        {
            OrderGoods = new HashSet<OrderGoods>();
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Price { get; set; }
        public int PaymentStatus { get; set; }
        public int DeliverStatus { get; set; }

        public virtual CustomerEntity Customer { get; set; }
        public virtual ICollection<OrderGoods> OrderGoods { get; set; }
    }
}
