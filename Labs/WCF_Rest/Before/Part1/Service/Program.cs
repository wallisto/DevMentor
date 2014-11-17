using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace WcfBindings
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(BookService)))
            {
                host.Open();
                Console.WriteLine("Service is running.\nPress any key to exit ...");
                Console.ReadKey(true);
            }
        }
    }
}
