using System.Data.Entity;
using System.Linq;
using AcmeCorpDomainObjects;

namespace AcmeCorpEF
{
    public class EFRepository<T> : IRepository<T> where T:class
    {
        protected DbSet<T> objectSet;

        public EFRepository(DbContext context)
        {
            objectSet = context.Set<T>();
        }

        public virtual  IQueryable<T> Entities
        {
            get { return objectSet; }
        }

        public virtual void Add(T entity)
        {
            objectSet.Add(entity);
        }

        public virtual void Remove(T entity)
        {
            objectSet.Remove(entity);
        }
    }

    public class EFPurchaseOrderRepository : EFRepository<PurchaseOrder>, IPurchaseOrderRepository
    {
        public EFPurchaseOrderRepository(DbContext context) : base(context)
        {
        }

        public override IQueryable<PurchaseOrder> Entities
        {
            get
            {
                return objectSet.Include("Supplier").Include("LineItems");
            }
        }
    }
}