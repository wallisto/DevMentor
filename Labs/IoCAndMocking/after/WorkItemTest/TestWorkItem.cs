using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkTrekModel;

namespace WorkItemTest
{
    [TestClass]
    public class TestWorkItem
    {
        [TestMethod]
        public void ShouldCreateAWorkItem_WhenCreatedOnDate_IsInThePast()
        {
            DateTime createdOn = new DateTime(2009, 1, 1);
            WorkItem item = new WorkItem("", "", createdOn);

            Assert.AreEqual(item.CreatedOn, createdOn);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ShouldThrowAnException_WhenWorkItemCreatedOnDate_IsInTheFuture()
        {
            DateTime createdOn = DateTime.Now + new TimeSpan(1, 0, 0, 0);
            WorkItem item = new WorkItem("", "", createdOn);
        }
    
        [TestMethod]
        public void ShouldCreateWorkItem_WhenCreatedOnDate_IsToday()
        {
            DateTime createdOn = DateTime.Now;
            WorkItem item = new WorkItem("", "", createdOn);

            Assert.AreEqual(item.CreatedOn, createdOn);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ShouldThrowAnException_WhenWorkItemFinishedOnDate_IsInTheFuture()
        {
            DateTime createdOn = DateTime.Now;
            WorkItem item = new WorkItem("", "", createdOn);

            item.FinishedOn = DateTime.Now + new TimeSpan(1, 0, 0, 0);
        }

        [TestMethod]
        public void ShouldNotThrowAnException_WhenWorkItemFinishedOnDate_IsNull()
        {
            DateTime createdOn = DateTime.Now;
            WorkItem item = new WorkItem("", "", createdOn);

            item.FinishedOn = null;
            Assert.IsNull(item.FinishedOn);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ShouldThrowAnException_WhenWorkItemFinishedOnDate_IsBeforeCreatedOnDate()
        {
            DateTime createdOn = DateTime.Now;
            WorkItem item = new WorkItem("", "", createdOn);

            item.FinishedOn = DateTime.Now - new TimeSpan(1, 0, 0, 0);
        }

        [TestMethod]
        public void ShouldSetFinishedOnDate_WhenWorkItemFinishedOnDate_IsEqualToCreatedOnDate()
        {
            DateTime createdOn = DateTime.Now;
            WorkItem item = new WorkItem("", "", createdOn);

            item.FinishedOn = DateTime.Now;
            Assert.IsTrue(item.CreatedOn <= item.FinishedOn);
        }

        [TestMethod]
        public void ShouldSetFinishedOnDate_WhenWorkItemFinishedOnDate_IsAfterCreatedOnDate()
        {
            DateTime createdOn = DateTime.Now - new TimeSpan(1, 0, 0, 0);
            WorkItem item = new WorkItem("", "", createdOn);

            item.FinishedOn = DateTime.Now;

            Assert.IsTrue(item.CreatedOn <= item.FinishedOn);
        }
    }
}
