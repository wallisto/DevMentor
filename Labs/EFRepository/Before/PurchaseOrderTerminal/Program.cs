using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AcmeCorpServices;
using AcmeCorpEF;

namespace PurchaseOrderTerminal
{
    class Program
    {
        static void Main(string[] args)
        {
            ITerminal terminal = new VT100();
            IPurchaseOrderService purchaseOrderService = new PurchaseOrderService();

            PurchaseOrderScreen screen = new PurchaseOrderScreen(purchaseOrderService , terminal);

            screen.Display();
           
        
        }

      

    }
}
