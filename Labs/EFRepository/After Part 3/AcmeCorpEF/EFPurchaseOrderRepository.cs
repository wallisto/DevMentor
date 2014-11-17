using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AcmeCorpEntities;
using System.Data.Objects;

namespace AcmeCorpEF
{
    public class EFPurchaseOrderRepository : IPurchaseOrderRepository
    {
        protected ObjectSet<PurchaseOrder> objectSet;

        public EFPurchaseOrderRepository(ObjectContext ctx)
        {
            objectSet = ctx.CreateObjectSet<PurchaseOrder>();
        }

        public IQueryable<PurchaseOrder> Entities
        {
            get { return objectSet.Include("Supplier").Include("LineItems"); }
        }

        public void Add(PurchaseOrder order)
        {
            objectSet.AddObject(order);
        }

        public void Remove(PurchaseOrder order)
        {
            objectSet.DeleteObject(order);
        }
    }
}
