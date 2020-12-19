using System;

namespace Billing.WebApi.Client.Models
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
