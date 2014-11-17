using System;

namespace AcmeCorpDomainObjects
{
    public interface IUnitOfWork : IDisposable
    {
        IPurchaseOrderRepository PurchaseOrders { get;  }

        void Commit();
    }
}