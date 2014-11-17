using System.Data.Entity;
using AcmeCorpDomainObjects;

namespace AcmeCorpEF
{
    public class AcmeCorpContainer : DbContext
    {
        public AcmeCorpContainer() : base("AcmeCorpEntities")
        {
            
        }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderLineItem> PurchaseOrderLineItems { get; set; }
    }
}