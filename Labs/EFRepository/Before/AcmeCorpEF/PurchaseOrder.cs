using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AcmeCorpEF
{
    public partial class PurchaseOrder
    {
     

        public IEnumerable<PurchaseOrderLineItem> Items { get { return LineItems; } }

        public decimal TotalSpent { get { return Items.Sum(li => li.Total); } }

        public bool IsInBudget(PurchaseOrderLineItem item)
        {
            return (item.Total + TotalSpent <= MaxValue);
        }

        public void AddLineItem(PurchaseOrderLineItem item)
        {
            if (!IsInBudget(item))
            {
                throw new InvalidOperationException("Item exceeds budget");
            }

            LineItems.Add(item);
        }

        public void RemoveLineItem(int id)
        {
            LineItems.Remove(LineItems.Where(li => li.Id == id).Single());
        }
    }
}
