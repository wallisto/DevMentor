using System;
using Microsoft.IdentityModel.Configuration;
using Microsoft.IdentityModel.Protocols.WSTrust;
using System.ServiceModel.Description;
using DM;

namespace PetShopSTS
{
    class Program
    {
        static void Main(string[] args)
        {
            SecurityTokenServiceConfiguration config = new PetShopSTSConfiguration();

            using (WSTrustServiceHost host = new WSTrustServiceHost(config))
            {
#if DEBUG
                Console.WriteLine("Including exception details in faults");
                var serviceDebugBehavior = host.Description.Behaviors.Find<ServiceDebugBehavior>();
                serviceDebugBehavior.IncludeExceptionDetailInFaults = true;
#endif

                host.Open();
                
                Console.Title = host.Description.Endpoints[0].Address.Uri.ToString();
                Console.WriteLine("PetShopSTS started, press ENTER to stop ...");
                Console.ReadLine();
            }

        }
    }
}
