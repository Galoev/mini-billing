﻿using Billing.WebApi.Client.Models;
using Billing.WebApi.Console.Models;

namespace Billing.WebApi.Console.Converters
{
    public class CustomerConverter
    {
        public static Customer FromDto(CustomerDto customerDto) => new Customer
        {
            Id = customerDto.Id,
            Phone = customerDto.Phone,
            Name = customerDto.Name,
            AdditionalInfo = customerDto.AdditionalInfo ?? string.Empty
        };

        public static CustomerDto ToDto(Customer customer) => new CustomerDto
        {
            Id = customer.Id,
            Phone = customer.Phone,
            Name = customer.Name,
            AdditionalInfo = string.IsNullOrEmpty(customer.AdditionalInfo)
                    ? null
                    : customer.AdditionalInfo
        };
    }
}
