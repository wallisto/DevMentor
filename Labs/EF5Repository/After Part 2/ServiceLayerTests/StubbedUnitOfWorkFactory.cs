using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AcmeCorpDomainObjects;

namespace ServiceLayerTests
{
    class StubbedUnitOfWorkFactory : IUnitOfWorkFactory , IUnitOfWork
    {
        private AcmeCorpDomainObjects.IPurchaseOrderRepository fakePurchaseOrderRepository;

        public StubbedUnitOfWorkFactory(AcmeCorpDomainObjects.IPurchaseOrderRepository fakePurchaseOrderRepository)
        {
            // TODO: Complete member initialization
            this.fakePurchaseOrderRepository = fakePurchaseOrderRepository;
        }

        public IPurchaseOrderRepository PurchaseOrders
        {
            get { return fakePurchaseOrderRepository;  }
        }

        public bool Committed { get; set; }
        public void Commit()
        {
            Committed = true;
            return;
        }

        public void Dispose()
        {
            return;
        }

        public IUnitOfWork Create()
        {
            return this;
        }
    }
}
