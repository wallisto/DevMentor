using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkTrekModel;
using Repository;

namespace WorkTrekRepository
{
    public interface IWorkItemRepository : IRepository<WorkItem>
    {
    }
}
