using System.Collections.Generic;
using ThirdParty;

namespace Services
{
    public class SMSService : IDeliveryService
    {
        private readonly ISMSGateway gateway;

        public SMSService(ISMSGateway gateway)
        {
            this.gateway = gateway;
        }

        public void Send(IEnumerable<Contact> contacts, string messageTemplate)
        {
            foreach (Contact contact in contacts)
            {
                string message = string.Format(messageTemplate, contact.FirstName);

                gateway.Send(contact.CellPhone, message);
            }
        }
 
    }
}