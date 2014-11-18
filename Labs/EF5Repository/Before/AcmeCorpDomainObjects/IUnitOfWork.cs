using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmeCorpDomainObjects
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
