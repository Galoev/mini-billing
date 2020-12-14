﻿using Billing.WebApi.Client.Models;

namespace Billing.WebApi.Models
{
    public interface IOrderConverter
    {
        public OrderDto ToDto(Order order);
        public Order FromDto(OrderDto orderDto);
    }
}
