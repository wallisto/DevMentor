using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ContactRepository : IContactRepository
    {
        private List<Contact> contacts = GetContacts();

        public IEnumerable<Contact> GetAll()
        {
            return contacts;
        }

        public void Add(Contact contact)
        {
            contacts.Add(contact);
        }

        public void Remove(Contact contact)
        {
            contacts.Remove(contact);
        }

        public void Commit()
        {
        }

        private static List<Contact> GetContacts()
        {
            return new List<Contact>()
                       {
                           new Contact{FirstName = "Rich", LastName = "Blewett", Email = "richardb@develop.com", CellPhone = "+44723892833"},
                           new Contact{FirstName = "Andy", LastName = "Clymer", Email = "aclymer@develop.com", CellPhone = "+44712232123"},
                           new Contact{FirstName = "Kevin", LastName = "Jones", Email = "kevinj@develop.com", CellPhone = "+44707023419"},
                           new Contact{FirstName = "Simon", LastName = "Horrell", Email = "simonh@develop.com", CellPhone = "+44755998134"},
                           new Contact{FirstName = "Mark", LastName = "Smith", Email = "marksm@develop.com", CellPhone = "+1677234234"},
                           new Contact{FirstName = "Brock", LastName = "Allen", Email = "ballen@develop.com", CellPhone = "+1514765110"},
                       };
        }
    }
}
