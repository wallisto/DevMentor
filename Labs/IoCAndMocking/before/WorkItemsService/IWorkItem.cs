using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkTrekModel;
using System.ServiceModel;

namespace WorkItemsService
{
    [ServiceContract]
    public interface IWorkItem
    {
        [OperationContract]
        IEnumerable<WorkItem> GetItems(int limit);
    }
}
