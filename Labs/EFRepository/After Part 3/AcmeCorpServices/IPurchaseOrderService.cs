﻿using System;
using System.Collections.Generic;
using AcmeCorpEntities;

namespace AcmeCorpServices
{
    public interface IPurchaseOrderService
    {
        void AddToPurchaseOrder(int purchaseOrderId, string item, int quantity, decimal price);
        List<PurchaseOrder> GetAllOpenPurchaseOrders();
        PurchaseOrder GetPurchaseOrder(int purchaseOrderId);
    }
}
