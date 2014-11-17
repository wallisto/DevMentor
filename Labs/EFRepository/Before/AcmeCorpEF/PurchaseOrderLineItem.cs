using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AcmeCorpEF
{
    public partial class PurchaseOrderLineItem
    {
       

        public decimal Total { get { return Price * Quantity; } }
    }
}
