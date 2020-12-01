using Billing.WebApi.Models;

namespace Billing.WebApi.DAL.EFCore
{
    public class EFCoreStorage : IStorage
    {
        private BillingContext context;
        private EFCoreRepository<Order> orders;
        
        public EFCoreStorage(BillingContext context)
        {
            this.context = context;
        }

        public IGenericRepository<Order> Orders
        {
            get
            {
                if (orders == null)
                {
                    orders = new EFCoreRepository<Order>(context);
                }
                return orders;
            }
        }

        public void Commit()
        {
            context.SaveChanges();
        }

    }
}
