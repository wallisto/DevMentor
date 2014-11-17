using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AcmeCorpServices;
using AcmeCorpEntities;
using System.Reflection;
using AcmeCorpEF;

namespace PurchaseOrderTerminal
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly.Load("AcmeCorpEF");

            ITerminal terminal = new VT100();
            IPurchaseOrderService purchaseOrderService = new PurchaseOrderService(new EFUnitOfWorkFactory() );

            PurchaseOrderScreen screen = new PurchaseOrderScreen(purchaseOrderService , terminal);

            screen.Display();
           
        
        }

      

    }
}
