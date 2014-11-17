using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;

namespace DM.PetShop
{
    public class OrderService : IOrderContract
    {
        public void PlaceOrder(Order orderData)
        {
            Console.WriteLine("Order placed: {0} {1}",
                orderData.Quantity, orderData.Product);
        }
    }
}
