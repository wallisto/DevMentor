using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using WorkTrekRepository;
using WorkTrekModel;
using WorkItemsService;

namespace WorkItemsServiceTest
{
    [TestClass]
    public class WorkItemsServiceTest
    {
        [TestMethod]
        public void ShouldReturnTwoItems_WhenGetItems_IsAskedForTwoItems()
        {
            IWorkItemRepository repository = MockRepository.GenerateStub<IWorkItemRepository>();
            List<WorkItem> actual = new List<WorkItem> { 
                new WorkItem("", "", DateTime.Now), 
                new WorkItem("", "", DateTime.Now), 
                new WorkItem("", "", DateTime.Now), 
            };

            repository.Stub(r => r.Entities).Return(actual.AsQueryable());

            WorkItemServiceImpl service = new WorkItemServiceImpl(repository);
            int limit = 2;
            IEnumerable<WorkItem> expected = service.GetItems(limit);

            Assert.AreEqual(expected.Count(), limit);
        }
    }
}
