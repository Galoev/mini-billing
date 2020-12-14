using Billing.WebApi.Models;

namespace Billing.WebApi.Repositories
{
    public interface IOrdersRepository
    {
        decimal Create(Order orderToCreate);

        Order Get(int orderId);

        void Update(Order orderToUpdate);

        void Delete(Order orderToDelete);

        void Delete(int id);
    }
}
