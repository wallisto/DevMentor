using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;

namespace ServiceHost1
{
    class ServiceHost1App
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(DM.PetShop.OrderService)))
            {
                host.Open();

                Console.WriteLine("{0} is running ...", host.Description.Endpoints[0].Address);
                Console.ReadLine();
            }
        }
    }
}
