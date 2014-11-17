using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AcmeCorpEntities;

namespace AcmeCorpServices
{
    public class PurchaseOrderService : AcmeCorpServices.IPurchaseOrderService
    {
        private IUnitOfWorkFactory uwFactory;

        public PurchaseOrderService(IUnitOfWorkFactory uwFactory)
        {
            this.uwFactory = uwFactory;
        }

        public List<PurchaseOrder> GetAllOpenPurchaseOrders()
        {
            using (IUnitOfWork uw = uwFactory.Create())
            {
                return (from order in uw.PurchaseOrders.Entities
                        select order).ToList();

            }
        }


        public PurchaseOrder GetPurchaseOrder(int purchaseOrderId )
        {
            using (IUnitOfWork uw = uwFactory.Create() )
            {
                return (from order in uw.PurchaseOrders.Entities
                        where order.Id == purchaseOrderId
                        select order).Single();
            }

        }

        public void AddToPurchaseOrder(int purchaseOrderId, string item, int quantity, decimal price)
        {
            using (IUnitOfWork uw = uwFactory.Create())
            {
                PurchaseOrder order = uw.PurchaseOrders.Entities.Where(o => o.Id == purchaseOrderId).Single();

                order.AddLineItem(new PurchaseOrderLineItem() { Description = item, Quantity = quantity, Price = price, Position = order.Items.Count() });

                uw.Commit() ;
            }
        }


    }
}
