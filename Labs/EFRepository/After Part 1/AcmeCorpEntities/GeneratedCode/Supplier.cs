//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace AcmeCorpEntities
{
    public partial class Supplier
    {
        #region Primitive Properties
    
        public virtual int Id
        {
            get;
            set;
        }
    
        public virtual string Name
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual ICollection<PurchaseOrder> PurchaseOrders
        {
            get
            {
                if (_purchaseOrders == null)
                {
                    var newCollection = new FixupCollection<PurchaseOrder>();
                    newCollection.CollectionChanged += FixupPurchaseOrders;
                    _purchaseOrders = newCollection;
                }
                return _purchaseOrders;
            }
            set
            {
                if (!ReferenceEquals(_purchaseOrders, value))
                {
                    var previousValue = _purchaseOrders as FixupCollection<PurchaseOrder>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupPurchaseOrders;
                    }
                    _purchaseOrders = value;
                    var newValue = value as FixupCollection<PurchaseOrder>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupPurchaseOrders;
                    }
                }
            }
        }
        private ICollection<PurchaseOrder> _purchaseOrders;

        #endregion
        #region Association Fixup
    
        private void FixupPurchaseOrders(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (PurchaseOrder item in e.NewItems)
                {
                    item.Supplier = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (PurchaseOrder item in e.OldItems)
                {
                    if (ReferenceEquals(item.Supplier, this))
                    {
                        item.Supplier = null;
                    }
                }
            }
        }

        #endregion
    }
}