using System.Collections.Generic;

namespace AcmeCorpDomainObjects
{
    public class Supplier
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }

        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
    }
}