using System;
namespace Billing.WebApi.Client.Models
{
    public class ComponentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Measure { get; set; }
    }
}
