using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkTrekModel;
using WorkTrekRepository;
using System.Configuration;
using Microsoft.Practices.Unity;

namespace WorkItemsService
{
    public class WorkItemServiceImpl : IWorkItem
    {
        IWorkItemRepository _repository;

        public WorkItemServiceImpl()
        {
            _repository = IoC.Container.Instance.Resolve<IWorkItemRepository>();
        }

        public WorkItemServiceImpl(IWorkItemRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<WorkItem> GetItems(int limit)
        {
            IEnumerable<WorkItem> list = _repository.Entities.Take(limit).ToList();
            return list;
        }
    }
}
