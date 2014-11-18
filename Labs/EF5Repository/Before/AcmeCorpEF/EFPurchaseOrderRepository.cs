using AcmeCorpDomainObjects;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmeCorpEF
{
    public class EFPurchaseOrderRepository : IPurchaseOrderRepository

    {
        public EFPurchaseOrderRepository(DbContext ctx)
        {
            this.objectSet = ctx.Set<PurchaseOrder>();
        }

        public DbSet<PurchaseOrder> objectSet { get; set; }

        public virtual IQueryable<PurchaseOrder> Entities
        {
            get { return objectSet.Include("Supplier").Include("LineItems"); }
        }

        public virtual void Add(PurchaseOrder entity)
        {
            objectSet.Add(entity);
        }

        public virtual void Remove(PurchaseOrder entity)
        {
            objectSet.Remove(entity);
        }
    }
}
