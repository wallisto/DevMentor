using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcmeCorpServices;
using AcmeCorpDomainObjects;

namespace ServiceLayerTests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class TestPurchaseOrderService
    {
        public TestPurchaseOrderService()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion


        // No database is harmed during the execution of this test
        [TestMethod]
        public void TestGetAllOpenPurchaseOrders()
        {
            IPurchaseOrderRepository fakePurchaseOrderRepository = new FakePurchaseOrderRepository( new PurchaseOrder[] {
                new PurchaseOrder { Id = 1, MaxValue = 1000, Status = "Open" },
                new PurchaseOrder { Id = 2, MaxValue = 400, Status = "Closed" },
                new PurchaseOrder { Id = 3, MaxValue = 200, Status = "Open" }
            });

            IUnitOfWorkFactory stubbedUwFactory = new StubbedUnitOfWorkFactory(fakePurchaseOrderRepository);

            IPurchaseOrderService sut = new PurchaseOrderService(stubbedUwFactory);

            List<PurchaseOrder> openPurchaseOrders = sut.GetAllOpenPurchaseOrders();

            Assert.AreEqual(2, openPurchaseOrders.Count);

        }
    }
}
