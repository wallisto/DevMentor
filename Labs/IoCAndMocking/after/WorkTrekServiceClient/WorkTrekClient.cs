using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkTrekServiceClient.WorkTrekReference;

namespace WorkTrekServiceClient
{
    class WorkTrekClient
    {
        static void Main(string[] args)
        {
            WorkItemClient proxy = new WorkItemClient();

            var items = proxy.GetItems(10);
            Console.WriteLine(items.Count());
        }
    }
}
