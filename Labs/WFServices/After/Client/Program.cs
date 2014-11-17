using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.ServiceReference1;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var proxy = new LoanServiceClient();

            ApplyResponse resp = proxy.Apply(6000, 60);

            Console.WriteLine("{0} : {1}", resp.ApplicationId, resp.MonthlyRepayment);

            Console.WriteLine("Loan approved: {0}", proxy.Confirm(resp.ApplicationId));
        }
    }
}
