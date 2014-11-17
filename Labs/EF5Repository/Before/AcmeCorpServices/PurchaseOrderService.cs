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
        public PurchaseOrderService()
        {

        }

        public List<PurchaseOrder> GetAllOpenPurchaseOrders()
        {
            using (AcmeCorpContainer ctx = new AcmeCorpContainer())
            {
                
                return (from order in ctx.PurchaseOrders.Include("LineItems").Include("Supplier")
                        select order).ToList();
            }
        }

        public PurchaseOrder GetPurchaseOrder(int purchaseOrderId )
        {
            using (AcmeCorpContainer ctx = new AcmeCorpContainer())
            {
                return (from order in ctx.PurchaseOrders.Include("LineItems").Include("Supplier")
                        where order.Id == purchaseOrderId
                        select order).Single();
            }

        }

        public void AddToPurchaseOrder(int purchaseOrderId, string item, int quantity, decimal price)
        {
            using (AcmeCorpContainer ctx = new AcmeCorpContainer())
            {
                PurchaseOrder order = ctx.PurchaseOrders.Where(o => o.Id == purchaseOrderId).Single();

                order.AddLineItem(new PurchaseOrderLineItem() { Description = item, Quantity = quantity, Price = price, Position = order.Items.Count() });
                
                ctx.SaveChanges();
            }
        }


    }
}
