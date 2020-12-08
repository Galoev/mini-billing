using System;
using System.Collections.Generic;

namespace Billing.WebApi.Client.Models
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Measure { get; set; }
        public List<Tuple<ComponentDto, int>> Components { get; set; }
    }
}
