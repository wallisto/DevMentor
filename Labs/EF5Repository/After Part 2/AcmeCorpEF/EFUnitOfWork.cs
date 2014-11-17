using System.Data.Entity;
using AcmeCorpDomainObjects;

namespace AcmeCorpEF
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private IPurchaseOrderRepository purchaseOrderRepository;
        private DbContext ctx;

        public EFUnitOfWork(string connectionString)
        {
            ctx = new DbContext(connectionString);
        }

        public void Dispose()
        {
            ctx.Dispose();
        }

        public IPurchaseOrderRepository PurchaseOrders
        {
            get { return purchaseOrderRepository ?? (purchaseOrderRepository = new EFPurchaseOrderRepository(ctx)); }
        }

        public void Commit()
        {
            ctx.SaveChanges();
        }
    }
}