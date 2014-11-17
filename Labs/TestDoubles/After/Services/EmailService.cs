using System.Collections.Generic;
using ThirdParty;

namespace Services
{
    public class EmailService : IDeliveryService 
    {
        private readonly string subject;
        private readonly IEmailGateway gateway;

        public EmailService(string subject, IEmailGateway gateway)
        {
            this.subject = subject;
            this.gateway = gateway;
        }

        public void Send(IEnumerable<Contact> contacts, string messageTemplate)
        {
            foreach (Contact contact in contacts)
            {
                string message = string.Format(messageTemplate, contact.FirstName);

                gateway.Send("notspam@spam.net", contact.Email, subject, message);
            }
        }
    }
}