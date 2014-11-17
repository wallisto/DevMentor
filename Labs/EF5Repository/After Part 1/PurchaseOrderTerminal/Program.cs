using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AcmeCorpDomainObjects;
using AcmeCorpEF;
using AcmeCorpServices;

namespace PurchaseOrderTerminal
{
    class Program
    {
        static void Main(string[] args)
        {
            IUnitOfWorkFactory uwf = new EFUnitOfWorkFactory();

            ITerminal terminal = new VT100();
            IPurchaseOrderService purchaseOrderService = new PurchaseOrderService(uwf);

            PurchaseOrderScreen screen = new PurchaseOrderScreen(purchaseOrderService , terminal);

            screen.Display();
           
        
        }

      

    }
}
