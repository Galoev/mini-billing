using Billing.WebApi.Models;
using Billing.WebApi.Repositories.Models;
using Billing.WebApi.Client.Utility;
using System;
using System.Linq;

namespace Billing.WebApi.Repositories
{
    public class ComponentRepository : IComponentRepository
    {
        private readonly BillingContext billingContext;

        public ComponentRepository(BillingContext billingContext)
        {
            this.billingContext = billingContext;
        }

        public Result<Component> Create(Component componentToCreate)
        {
            var componentDbo = new ComponentDbo
            {
                QuantityType = componentToCreate.QuantityType,
                UnitPrice = componentToCreate.UnitPrice,
                Description = componentToCreate.Description
            };

            billingContext.Components.Add(componentDbo);
            var createdRows = billingContext.SaveChanges();
            if (createdRows > 0)
            {
                componentToCreate.Id = componentDbo.Id;
                return new Result<Component>
                {
                    IsSuccess = true,
                    Message = "Component successfully created!",
                    Value = componentToCreate
                };
            }
            return new Result<Component>
            {
                IsSuccess = false,
                Message = "Component not created!"
            };
        }

        public Result<Component> Delete(Guid componentId)
        {
            var componentDbo = billingContext.Components.FirstOrDefault(c => c.Id == componentId);
            if (componentDbo == null)
            {
                return new Result<Component>
                {
                    IsSuccess = false,
                    Message = $"Component with id {componentId} not found!"
                };
            }

            var deletedComponent = new Component
            {
                Id = componentDbo.Id,
                UnitPrice = componentDbo.UnitPrice,
                QuantityType = componentDbo.QuantityType,
                Description = componentDbo.Description
            };

            billingContext.Components.Remove(componentDbo);
            var deletedRows = billingContext.SaveChanges();

            if (deletedRows > 0)
            {
                return new Result<Component>
                {
                    IsSuccess = true,
                    Message = "Component successfully deleted!",
                    Value = deletedComponent
                };
            }
            return new Result<Component>
            {
                IsSuccess = false,
                Message = "Component not deleted!"
            };
        }

        public Result<Component> Get(Guid componentId)
        {
            var componentDbo = billingContext.Components.FirstOrDefault(c => c.Id == componentId);
            if (componentDbo == null)
            {
                return new Result<Component>
                {
                    IsSuccess = false,
                    Message = $"Component with id {componentId} not found!"
                };
            }
            return new Result<Component>
            {
                IsSuccess = true,
                Message = $"Component with id {componentId} successfully found!",
                Value = new Component
                {
                    Id = componentDbo.Id,
                    QuantityType = componentDbo.QuantityType,
                    UnitPrice = componentDbo.UnitPrice,
                    Description = componentDbo.Description
                }
            };
        }

        public Result<Component> Update(Component componentToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
