using Billing.WebApi.Models;

namespace Billing.WebApi.DAL
{
    public interface IStorage
    {
            public IGenericRepository<Order> Orders { get; }
            public void Commit();
    }
}
