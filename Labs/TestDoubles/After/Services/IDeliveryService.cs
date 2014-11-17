using System.Collections.Generic;

namespace Services
{
    public interface IDeliveryService
    {
        void Send(IEnumerable<Contact> contacts, string messageTemplate);
    }
}