using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Services.Test
{
    [TestClass]
    public class CRMTests
    {
        [TestMethod]
        public void SendCustomerMessage_WhenInvoked_ShouldSendToEachContactFromRespository()
        {
//            var repo = new StubContactRepository();
            var contacts = new List<Contact>
                       {
                           new Contact(),
                           new Contact(),
                           new Contact(),
                       };

            var repo = MockRepository.GenerateStub<IContactRepository>();
            repo.Stub(r => r.GetAll())
                .Return(contacts);

            var deliveryService = new SpyDeliveryService();
            var crm = new CRM(repo, deliveryService, null);

            crm.SendCustomerMessage(DeliveryMethod.SMS, "");

            CollectionAssert.AreEquivalent(contacts, deliveryService.Contacts.ToList());

        } 
    }

    class StubContactRepository : IContactRepository
    {
        Contact[] contacts = new[]
                       {
                           new Contact(),
                           new Contact(),
                           new Contact(),
                       };
        public IEnumerable<Contact> GetAll()
        {
            return contacts; 
        }

        public void Add(Contact contact)
        {
            throw new NotImplementedException();
        }

        public void Remove(Contact contact)
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }
    }


    class SpyDeliveryService : IDeliveryService
    {
        public IEnumerable<Contact> Contacts { get; private set; }
        public void Send(IEnumerable<Contact> contacts, string messageTemplate)
        {
            Contacts = new List<Contact>(contacts);
        }
    }
}
