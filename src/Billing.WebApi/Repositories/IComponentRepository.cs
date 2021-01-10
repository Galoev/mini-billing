using Billing.WebApi.Models;
using Billing.WebApi.Client.Utility;
using System;

namespace Billing.WebApi.Repositories
{
    public interface IComponentRepository
    {
        Result<Component> Create(Component componentToCreate);

        Result<Component> Get(Guid componentId);

        Result<Component> Update(Component componentToUpdate);

        Result<Component> Delete(Guid componentId);
    }
}
