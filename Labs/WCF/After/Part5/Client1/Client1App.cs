using System;
using System.Collections.Generic;
using System.Text;

using System.ServiceModel;
using System.ServiceModel.Channels;
using DM.PetShop;

namespace Client1
{
    class Client1App
    {
        static void Main(string[] args)
        {
            ChannelFactory<IOrderContract> factory =
                new ChannelFactory<IOrderContract>(new BasicHttpBinding());

            IOrderContract proxy = factory.CreateChannel
                (new EndpointAddress("http://localhost:9000/PetShop/OrderService"));

            using ((IDisposable)proxy)
            {
                proxy.PlaceOrder(new Order{ Quantity=1, Product="parrot" });
            }
        }
    }
}
