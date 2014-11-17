using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AcmeCorpDomainObjects;
using AcmeCorpEF;

namespace AcmeCorpServices
{
    public class PurchaseOrderService : AcmeCorpServices.IPurchaseOrderService
    {
        private readonly IUnitOfWorkFactory uwf;

        public PurchaseOrderService(IUnitOfWorkFactory uwf)
        {
            this.uwf = uwf;
        }

        public List<PurchaseOrder> GetAllOpenPurchaseOrders()
        {
            using (IUnitOfWork uw = uwf.Create())
            {
                return (from order in uw.PurchaseOrders.Entities select order).ToList();
            }
        }

        public PurchaseOrder GetPurchaseOrder(int purchaseOrderId )
        {
            using (IUnitOfWork uw = uwf.Create())
            {
                return uw.PurchaseOrders.Entities.Single(o => o.Id == purchaseOrderId);
            }

        }

        public void AddToPurchaseOrder(int purchaseOrderId, string item, int quantity, decimal price)
        {
           using (IUnitOfWork uw = uwf.Create())
            {
                PurchaseOrder order = uw.PurchaseOrders.Entities.Single(o => o.Id == purchaseOrderId);
            
                
                order.AddLineItem(new PurchaseOrderLineItem() { Description = item, Quantity = quantity, Price = price, Position = order.Items.Count() });
                
                uw.Commit();
            }
        }


    }
}
