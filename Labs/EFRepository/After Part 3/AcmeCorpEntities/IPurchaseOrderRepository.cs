using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AcmeCorpEntities
{
    public interface IPurchaseOrderRepository
    {
        IQueryable<PurchaseOrder> Entities { get; }

        void Add(PurchaseOrder order);
        void Remove(PurchaseOrder order);
    }
}
