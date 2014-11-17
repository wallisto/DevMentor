using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DM.Collections;

namespace DM.Collections.Test
{
    [TestClass]
    public class PriorityQueueTests
    {
        private PriorityQueue<int> q;
        [TestInitialize]
        public void Setup()
        {
            q = new PriorityQueue<int>();
        }

        [TestMethod]
        public void Ctor_WhenCreated_ShouldHaveCount0()
        {
            Assert.AreEqual(0, q.Count);
        }

        [TestMethod]
        public void Enqueue_WhenPassedAnItem_ShouldIncrementCountBy1()
        {
            q.Enqueue(5);

            Assert.AreEqual(1, q.Count);
        }

        [TestMethod]
        public void Enqueue_WhenCalledMultipleTimes_ShouldIncrementCountForEachCall()
        {
            q.Enqueue(5);
            q.Enqueue(42);
            q.Enqueue(23);

            Assert.AreEqual(3, q.Count);
        }

        [TestMethod]
        public void Dequeue_WhenCalledAfterEnqueue_ShouldReturnCountToInitialValue()
        {
            int count = q.Count;
            q.Enqueue(5);

            q.Dequeue();

            Assert.AreEqual(count, q.Count);
        }

        [TestMethod]
        public void Dequeue_WhenCalledAfterEnqueue_ShouldReturnEnqueuedValue()
        {
            int val = 5;
            q.Enqueue(val);

            int result = q.Dequeue();

            Assert.AreEqual(val, result);
        }

        [TestMethod]
        public void Dequeue_WhenTwoItemsEnqueued_ShouldReturnFirstValue()
        {
            int first = 5;
            int second = 42;

            q.Enqueue(first);
            q.Enqueue(second);

            int result = q.Dequeue();

            Assert.AreEqual(first, result);
        }

        [TestMethod]
        public void Dequeue_WhenTwoItemsEnqueuedAndDequeuedTwice_ShouldReturnSecondValue()
        {
            int first = 5;
            int second = 42;
            q.Enqueue(first);
            q.Enqueue(second);

            q.Dequeue();
            int result = q.Dequeue();

            Assert.AreEqual(second, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Dequeue_WhenQueueIsEmpty_ShouldThrowAnExcedption()
        {
            q.Dequeue();
        }

        [TestMethod]
        public void Dequeue_WhenEnqueueWithHighPriority_ShouldDequeueHighPriorityFirst()
        {
            int val = 42;
            q.Enqueue(5);
            q.Enqueue(val, Priority.High);

            int result = q.Dequeue();

            Assert.AreEqual(val, result);
        }

        [TestMethod]
        public void Dequeue_WhenEnqueueWithLowPriority_ShouldDequeueAfterNormal()
        {
            int val = 42;
            q.Enqueue(5, Priority.Low);
            q.Enqueue(val);

            int result = q.Dequeue();

            Assert.AreEqual(val, result);
        }
    }
}
 