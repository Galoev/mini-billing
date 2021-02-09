using Billing.WebApi.Models;
using Billing.WebApi.Client.Utility;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Billing.WebApi.Repositories.Models;

namespace Billing.WebApi.Repositories
{
    public interface IGoodsRepository
    {
        Result<Good> Create(Good goodToCreate);

        Result<Good> Get(Guid goodId);

        Result<List<Good>> Get();

        Result<List<Good>> Get(Expression<Func<GoodDbo, bool>> predicate);

        Result<Good> Update(Good goodToUpdate);

        Result<Good> Delete(Guid goodId);
    }
}
