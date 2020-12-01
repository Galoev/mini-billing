using System;

namespace Billing.WebApi.Models
{
    public class Order
    {
        public int ID { get; set; }
        public string Client { get; set; }

        public DateTime OrderDate { get; set; }

        public long Price { get; set; }
        public int BillingStatus { get; set; }
        public int DeliverStatus { get; set; }
    }
}
