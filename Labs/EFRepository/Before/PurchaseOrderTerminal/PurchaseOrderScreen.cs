using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AcmeCorpServices;
using AcmeCorpEF;

namespace PurchaseOrderTerminal
{
    public class PurchaseOrderScreen 
    {
        private ITerminal terminal;
        private IPurchaseOrderService purchaseOrderService;

        public PurchaseOrderScreen(IPurchaseOrderService purchaseOrderService , ITerminal terminal)
        {
            this.terminal = terminal;
            this.purchaseOrderService = purchaseOrderService;
        }

        public void Display()
        {
            DisplayPurchaseOrderSummary();
        }

        private void DisplayPurchaseOrderSummary()
        {

            bool quit = false;

            while (!quit)
            {
                terminal.Clear();
                terminal.WriteLine("Open Purchase Orders");
                terminal.WriteLine();

                foreach (PurchaseOrder order in purchaseOrderService.GetAllOpenPurchaseOrders())
                {
                    terminal.WriteLine("{0} : {2:C} / {1:C} for Supplier {3}",
                        order.Id,
                        order.MaxValue,
                        order.TotalSpent,
                        order.Supplier.Name);

                }

                terminal.WriteLine();
                terminal.WriteLine();

                terminal.WriteLine("[L]ist order [Q]uit");
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.L:
                        {
                            terminal.Write("Enter id : ");
                            int id = int.Parse(Console.ReadLine());

                            DisplayPurchaseOrder( id);

                        } break;

                    case ConsoleKey.Q:
                        {
                            quit = true;
                        } break;

                }
            }

        }

        private void DisplayPurchaseOrder( int id)
        {

         
            while (true)
            {
                PurchaseOrder order = purchaseOrderService.GetPurchaseOrder(id);

                terminal.Clear();

                terminal.WriteLine("Purchase Order {0} for {1}\n", order.Id, order.Supplier.Name);

                foreach (PurchaseOrderLineItem item in order.Items)
                {
                    Console.WriteLine("{0} : {1} x {2} @ {3:C} = {4:C}", item.Id, item.Description, item.Quantity, item.Price, item.Total);
                }

                Console.WriteLine("\n\nAmount left to spend : {0:C}\n", order.MaxValue - order.TotalSpent);


                terminal.WriteLine();
                terminal.WriteLine();

                terminal.WriteLine("[A]dd to order [B]ack");
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.A:
                        {
                            terminal.Write("Enter product to add : ");
                            string product = Console.ReadLine();
                            terminal.Write("Enter quantity :");
                            int quantity = int.Parse(Console.ReadLine());
                            terminal.Write("Enter price : ");
                            decimal price = decimal.Parse(Console.ReadLine());

                            purchaseOrderService.AddToPurchaseOrder(id, product, quantity, price);

                        } break;

                    case ConsoleKey.B:
                        return;



                }
            }
        }

        private void AddToPurchaseOrder( int orderId)
        {
            terminal.Clear();

            Console.Write("Product : ");
            string product = Console.ReadLine();

            Console.Write("Price : ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.Write("Quantity : ");
            int quantity = int.Parse(Console.ReadLine());

          

            purchaseOrderService.AddToPurchaseOrder(orderId, product, quantity, price);

        }
    }
}
