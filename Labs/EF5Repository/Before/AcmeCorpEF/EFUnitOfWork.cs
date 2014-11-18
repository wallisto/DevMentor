using AcmeCorpDomainObjects;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmeCorpEF
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private DbContext ctx;
        private IPurchaseOrderRepository _iPurchaseOrderRepository;
        public EFUnitOfWork(string connectionString)
        {
            this.ctx = new DbContext(connectionString);
            this._iPurchaseOrderRepository = new EFPurchaseOrderRepository(ctx);
        }

        public IPurchaseOrderRepository PurchaseOrders
        {
            get { return _iPurchaseOrderRepository; }
        }

        public void Commit()
        {
            ctx.SaveChanges();
        }

        public void Dispose()
        {
            ctx.Dispose();
        }
    }
}
