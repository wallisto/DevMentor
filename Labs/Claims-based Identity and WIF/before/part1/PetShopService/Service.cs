using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
using System.Linq;

namespace DM
{
    [ServiceBehavior(Name = "PetShopService", Namespace = Constants.SERVICE_NAMESPACE)]
    class PetShop : IPetShop
    {
        public void PlaceOrder(OrderMessage message)
        {
            Console.WriteLine("Order:");
            Console.WriteLine("Product name: {0}", message.Order.ProductName);
            Console.WriteLine("Quantity: {0}", message.Order.Quantity);
            Console.WriteLine("Comments: {0}", message.Order.Comments);
        }
    }
}
