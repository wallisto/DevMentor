using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkTrekModel;
using WorkTrekRepository;
using System.Configuration;

namespace WorkItemsService
{
    public class WorkItemServiceImpl : IWorkItem
    {
        public IEnumerable<WorkItem> GetItems(int limit)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["WorkTrekEntities"].ConnectionString;
            WorkItemRepository repository = new WorkItemRepository(connectionString);
            var list = repository.Entities.Take(limit).ToList();
            return list;
        }
    }
}
