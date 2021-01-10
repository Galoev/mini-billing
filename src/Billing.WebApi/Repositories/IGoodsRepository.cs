using Billing.WebApi.Models;
using Billing.WebApi.Utility;
using System;
using System.Collections.Generic;

namespace Billing.WebApi.Repositories
{
    public interface IGoodsRepository
    {
        Result<Good> Create(Good goodToCreate);

        Result<Good> Get(Guid goodId);

        Result<List<Good>> Get();

        Result<Good> Update(Good goodToUpdate);

        Result<Good> Delete(Guid goodId);
    }
}
