using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ContactRepository : IContactRepository
    {
        List<Contact> contacts;

        public ContactRepository()
        {
            contacts = new List<Contact>()
            {
                new Contact{ Id=1, Name="Richard Blewett", Email="richardb@develop.com", Notes="Needs typing lessons"},
                new Contact{ Id=2, Name="Andy Clymer", Email="aclymer@develop.com", Notes="Loves warming up the room"},
                new Contact{ Id=2, Name="Dave Wheeler", Email="davew@develop.com", Notes="Colouring-in guy"},
            };
        }

        public IEnumerable<Contact> All
        {
            get
            {
                return contacts;
            }
        }

        public void Add(Contact contact)
        {
            contacts.Add(contact);
        }

        public Contact GetById(int id)
        {
            return contacts.SingleOrDefault(c => c.Id == id);
        }

        public void Delete(int id)
        {
            contacts.RemoveAll(c => c.Id == id);
        }

    }
}
