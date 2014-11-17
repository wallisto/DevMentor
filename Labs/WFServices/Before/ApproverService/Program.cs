using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace ApproverService
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(ApproveService), new Uri("http://localhost:9000/approval"));
            host.AddServiceEndpoint(typeof(IApprove), new BasicHttpBinding(), "");

            host.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true });
            host.Open();

            Console.WriteLine("Service Ready ...");
            Console.ReadLine();
        }
    }

    class ApproveService : IApprove
    {
        public bool RequestApproval(double amount)
        {
            Random r = new Random();

            Console.WriteLine("Approval requested for loan of: {0}", amount);

            return r.Next(100) % 2 == 0;
        }
    }

    [ServiceContract]
    interface IApprove
    {
        [OperationContract]
        bool RequestApproval(double amount);
    }

}
