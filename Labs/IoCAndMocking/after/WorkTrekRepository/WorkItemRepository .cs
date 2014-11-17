using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkTrekModel;

namespace WorkTrekRepository
{
    public class WorkItemRepository : WorkTrekRepository<WorkItem>, IWorkItemRepository
    {
        public WorkItemRepository(string connectionString) 
            : base(connectionString)
        {

        }
    }
}
