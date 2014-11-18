using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DM.Collections;

namespace DM.Collections.Test
{
    [TestClass]
    public class PriorityQueueTest
    {
        private PriorityQueue<int> pQueue;

        [TestInitialize]
        public void Setup()
        {
            pQueue = new PriorityQueue<int>();
        }

        [TestMethod]
        public void Ctor_WhenCreated_ShouldHaveCount0()
        {
            Assert.AreEqual(0, pQueue.Count);
        }

        [TestMethod]
        public void Enqueue_WhenCalled_IncrementsCountByOne()
        {
            pQueue.Enqueue(1);

            Assert.AreEqual(1, pQueue.Count);
        }

        [TestMethod]
        public void Dequeue_WhenCalled_DecrementsCountByOne()
        {
            pQueue.Enqueue(1);
            pQueue.Enqueue(4);
            pQueue.Dequeue();

            Assert.AreEqual(1, pQueue.Count);
        }

        [TestMethod]
        public void Dequeue_WhenCalledAfterEnqueue_ShouldReturnCountToInitialValue()
        {
            int count = pQueue.Count;
            pQueue.Enqueue(5);

            pQueue.Dequeue();

            Assert.AreEqual(count, pQueue.Count);
        }

        [TestMethod]
        public void Dequeue_WhenCalledAfterEnqueue_ReturnsEnqueuedItem()
        {
            pQueue.Enqueue(5);

            var item = pQueue.Dequeue();

            Assert.AreEqual(5, item);
        }

        [TestMethod]
        public void Dequeue_WhenCalledAfterTwoEnqueues_ReturnsFirstEnqueuedItem()
        {
            pQueue.Enqueue(5);
            pQueue.Enqueue(23);

            var item = pQueue.Dequeue();

            Assert.AreEqual(5, item);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Dequeue_WhenQueueIsEmpty_ThrowsException()
        {
            pQueue.Dequeue();
        }
    }
}
