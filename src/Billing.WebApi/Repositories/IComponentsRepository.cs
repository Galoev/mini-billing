using Billing.WebApi.Models;
using Billing.WebApi.Client.Utility;
using System;
using System.Collections.Generic;

namespace Billing.WebApi.Repositories
{
    public interface IComponentsRepository
    {
        Result<Component> Create(Component componentToCreate);

        Result<Component> Get(Guid componentId);

        Result<List<Component>> Get();

        Result<Component> Update(Component componentToUpdate);

        Result<Component> Delete(Guid componentId);
    }
}
