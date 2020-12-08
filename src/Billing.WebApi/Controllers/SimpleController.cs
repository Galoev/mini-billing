using Microsoft.AspNetCore.Mvc;
using Billing.WebApi.Models;
using System.Collections.Generic;
using System;

namespace Billing.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimpleController : ControllerBase
    {
        // GET: api/simple
        [HttpGet]
        public ActionResult<MessageDTO> GetMessage(string name)
        {
            return new MessageDTO { message = $"Hello, {name}!" };
        }

        [HttpPost]
        public ActionResult<MessageDTO> PostMessage(MessageDTO msg)
        {

            return CreatedAtAction(
                nameof(GetMessage),
                new { msg.message},
                msg);
        }

        // GET: api/simple/order
        [HttpGet("order")]
        public ActionResult<OrderDto> GetOrderAsync(int id)
        {
            return new OrderDto
            {
                Id = id,
                Customer = new CustomerDto
                {
                    Name = "MyCustomer1",
                    PhoneNumber = "8(900) 777-77-77",
                    Info = "Some Info about MyCustomer1"
                },
                Date = new System.DateTime(),
                Products = null
            };
        }

        // GET: api/simple/orders
        [HttpGet("orders")]
        public ActionResult<List<OrderDto>> GetOrdersAsync()
        {
            List<OrderDto> orders = new List<OrderDto>();
            orders.Add(new OrderDto
            {
                Id = 1,
                Customer = new CustomerDto
                {
                    Name = "MyCustomer1",
                    PhoneNumber = "8(900) 777-77-77",
                    Info = "Some Info about MyCustomer1"
                },
                Date = new System.DateTime(),
                Products = null
            }
            );

            orders.Add(new OrderDto
            {
                Id = 2,
                Customer = new CustomerDto
                {
                    Name = "MyCustomer2",
                    PhoneNumber = "8(900) 777-77-77",
                    Info = "Some Info about MyCustomer2"
                },
                Date = new System.DateTime(),
                Products = null
            }
            );

            orders.Add(new OrderDto
            {
                Id = 3,
                Customer = new CustomerDto
                {
                    Name = "MyCustomer3",
                    PhoneNumber = "8(900) 777-77-77",
                    Info = "Some Info about MyCustomer3"
                },
                Date = new System.DateTime(),
                Products = null
            }
            );
            return orders;
        }

        // GET: api/simple/products
        [HttpGet("products")]
        public ActionResult<List<ProductDto>> GetProductsAsync()
        {
            List<ProductDto> products = new List<ProductDto>();
            products.Add(new ProductDto
            {
                Id = 8,
                Name = "Product1",
                Price = 456,
                Measure = "measure1",
                Components = new List<Tuple<ComponentDto, int>>
                {
                    Tuple.Create (new ComponentDto
                    {
                        Name = "Component1_Product1",
                        Price = 4,
                        Measure = "measure1"
                    }, 1),
                    Tuple.Create (new ComponentDto
                    {
                        Name = "Component2_Product1",
                        Price = 5,
                        Measure = "measure1"
                    }, 2),
                    Tuple.Create (new ComponentDto
                    {
                        Name = "Component3_Product1",
                        Price = 6,
                        Measure = "measure1"
                    }, 3),
                }
            });
            products.Add(new ProductDto
            {
                Id = 65,
                Name = "Product2",
                Price = 135,
                Measure = "measure2",
                Components = new List<Tuple<ComponentDto, int>>
                {
                    Tuple.Create (new ComponentDto
                    {
                        Name = "Component1_Product2",
                        Price = 1,
                        Measure = "measure1"
                    }, 1),
                    Tuple.Create (new ComponentDto
                    {
                        Name = "Component2_Product2",
                        Price = 3,
                        Measure = "measure1"
                    }, 2),
                    Tuple.Create (new ComponentDto
                    {
                        Name = "Component3_Product2",
                        Price = 4,
                        Measure = "measure1"
                    }, 3),
                }
            });
            products.Add(new ProductDto
            {
                Id = 213,
                Name = "Product3",
                Price = 460,
                Measure = "measure3",
                Components = new List<Tuple<ComponentDto, int>>
                {
                    Tuple.Create (new ComponentDto
                    {
                        Name = "Component1_Product3",
                        Price = 4,
                        Measure = "measure1"
                    }, 1),
                    Tuple.Create (new ComponentDto
                    {
                        Name = "Component2_Product3",
                        Price = 5,
                        Measure = "measure1"
                    }, 2),
                    Tuple.Create (new ComponentDto
                    {
                        Name = "Component3_Product3",
                        Price = 6,
                        Measure = "measure1"
                    }, 3),
                }
            });
            return products;
        }

        // GET: api/simple/customer
        [HttpGet("customer")]
        public ActionResult<CustomerDto> GetCustomerAsync(int id)
        {
            return new CustomerDto
                {
                    Name = "MyCustomer1",
                    PhoneNumber = "8(900) 777-77-77",
                    Info = "Some Info about MyCustomer1"
                };
        }


        // GET: api/simple/customers
        [HttpGet("customers")]
        public ActionResult<List<CustomerDto>> GetCustomersAsync()
        {
            List<CustomerDto> customers = new List<CustomerDto>();
            customers.Add(new CustomerDto
            {
                Name = "MyCustomer1",
                PhoneNumber = "8(900) 111-11-11",
                Info = "Some Info about MyCustomer1"
            });
            customers.Add(new CustomerDto
            {
                Name = "MyCustomer2",
                PhoneNumber = "8(900) 222-22-22",
                Info = "Some Info about MyCustomer2"
            });
            customers.Add(new CustomerDto
            {
                Name = "MyCustomer3",
                PhoneNumber = "8(900) 333-33-33",
                Info = "Some Info about MyCustomer3"
            });
            return customers;
        }

        // POST: api/simple/create/customer
        [HttpPost("create/customer")]
        public ActionResult<CustomerDto> PostCreateCustomer(CustomerDto customer)
        {
            return CreatedAtAction(nameof(GetCustomerAsync),
                new
                {
                    id = customer.Id,
                    customer.Name,
                    customer.PhoneNumber,
                    customer.Info
                },
                customer);
        }

        // POST: api/simple/create/order
        [HttpPost("create/order")]
        public ActionResult<OrderDto> PostCreateOrder(OrderDto order)
        {
            return CreatedAtAction(nameof(GetOrderAsync),
                new
                {
                    id = order.Id,
                    order.Customer,
                    order.Products,
                    order.Date,
                    order.PaymentStatus,
                    order.DeliverStatus
                },
                order);
        }
    }
}