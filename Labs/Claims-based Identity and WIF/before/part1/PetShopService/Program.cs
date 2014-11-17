using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace DM
{
    class Program
    {
        static void Main()
        {
            using (ServiceHost host = new ServiceHost(typeof(PetShop)))
            {
#if DEBUG
                Console.WriteLine("Including exception details in faults");
                var serviceDebugBehavior = host.Description.Behaviors.Find<ServiceDebugBehavior>();
                serviceDebugBehavior.IncludeExceptionDetailInFaults = true;
#endif

                host.Open();
                Console.Title = host.Description.Endpoints[0].Address.Uri.ToString();
                Console.WriteLine("PetShopService started, press ENTER to stop ...");
                Console.ReadLine();
            }
        }
    }
}
