using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AcmeCorpEntities
{
    public interface IUnitOfWork : IDisposable
    {
        IPurchaseOrderRepository PurchaseOrders { get; }

        void Commit();
    }

    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}
