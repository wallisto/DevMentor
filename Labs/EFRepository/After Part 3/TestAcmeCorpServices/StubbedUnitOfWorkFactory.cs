using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AcmeCorpEntities;

namespace TestAcmeCorpServices
{
    class StubbedUnitOfWorkFactory : IUnitOfWorkFactory , IUnitOfWork
    {
        private AcmeCorpEntities.IPurchaseOrderRepository fakePurchaseOrderRepository;

        public StubbedUnitOfWorkFactory(AcmeCorpEntities.IPurchaseOrderRepository fakePurchaseOrderRepository)
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
