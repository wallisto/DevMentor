using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AcmeCorpEntities;
using System.Data.Objects;

namespace AcmeCorpEF
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private ObjectContext ctx;
        private IPurchaseOrderRepository purchaseOrderRepository;

        public EFUnitOfWork(string connectionString)
        {
            this.ctx = new ObjectContext(connectionString);
            this.purchaseOrderRepository = new EFPurchaseOrderRepository(ctx);
           
        }

     
        public IPurchaseOrderRepository PurchaseOrders
        {
            get { return purchaseOrderRepository;  }
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
