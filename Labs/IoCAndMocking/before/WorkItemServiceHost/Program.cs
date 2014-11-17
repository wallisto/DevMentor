using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using WorkItemsService;

namespace WorkItemServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(WorkItemServiceImpl), new Uri("http://localhost:9000/WorkTrek")))
            {
                host.Open();
                Console.WriteLine("Service ready");
                Console.ReadLine();
            }
        }
    }
}
