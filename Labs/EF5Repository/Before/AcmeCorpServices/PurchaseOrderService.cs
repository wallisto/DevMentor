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
        IUnitOfWorkFactory _uowFactory;
        public PurchaseOrderService(IUnitOfWorkFactory uowFactory)
        {
            this._uowFactory = uowFactory;
        }

        public List<PurchaseOrder> GetAllOpenPurchaseOrders()
        {
            using (IUnitOfWork uw = _uowFactory.Create())
            {

                return (from order in uw.PurchaseOrders.Entities
                        select order).ToList();
            }
        }

        public PurchaseOrder GetPurchaseOrder(int purchaseOrderId )
        {
            using (IUnitOfWork uw = _uowFactory.Create())
            {
                return (from order in uw.PurchaseOrders.Entities
                        where order.Id == purchaseOrderId
                        select order).Single();
            }

        }

        public void AddToPurchaseOrder(int purchaseOrderId, string item, int quantity, decimal price)
        {
            using (IUnitOfWork uw = _uowFactory.Create())
            {
                PurchaseOrder order = uw.PurchaseOrders.Entities.Where(o => o.Id == purchaseOrderId).Single();

                order.AddLineItem(new PurchaseOrderLineItem() { Description = item, Quantity = quantity, Price = price, Position = order.Items.Count() });

                uw.Commit();
            }
        }


    }
}
