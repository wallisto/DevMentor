using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AcmeCorpEntities;

namespace TestAcmeCorpServices
{
    class FakePurchaseOrderRepository : IPurchaseOrderRepository
    {
        private AcmeCorpEntities.PurchaseOrder[] purchaseOrders;

        public FakePurchaseOrderRepository(AcmeCorpEntities.PurchaseOrder[] purchaseOrders)
        {
            // TODO: Complete member initialization
            this.purchaseOrders = purchaseOrders;
        }

        public IQueryable<PurchaseOrder> Entities
        {
            get { return purchaseOrders.AsQueryable();  }
        }

        public void Add(PurchaseOrder order)
        {
            throw new NotImplementedException();
        }

        public void Remove(PurchaseOrder order)
        {
            throw new NotImplementedException();
        }
    }
}
