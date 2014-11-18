using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmeCorpDomainObjects
{
    public interface IPurchaseOrderRepository
    {
        IQueryable<PurchaseOrder> Entities { get; }

        void Add(PurchaseOrder entity);
        void Remove(PurchaseOrder entity);
    }
}
