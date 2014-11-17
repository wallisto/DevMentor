using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AcmeCorpDomainObjects
{
    public partial class PurchaseOrderLineItem
    {

        public virtual int Id { get; set; }
        public virtual string Description { get; set; }
        public virtual decimal Price { get; set; }
        public virtual int Quantity { get; set; }
        public virtual int PurchaseOrderId { get; set; }
        public virtual int Position { get; set; }

        public virtual PurchaseOrder PurchaseOrder { get; set; }

        public decimal Total { get { return Price * Quantity; } }
    }
}
