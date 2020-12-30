using System;

namespace Billing.WebApi.Repositories.Models
{
    public partial class CustomerDbo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
