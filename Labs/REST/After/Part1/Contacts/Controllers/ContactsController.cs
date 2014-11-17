using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Repository;

namespace Contacts.Controllers
{
    public class ContactsController : ApiController
    {
        IContactRepository repository;

        public ContactsController()
        {
            repository = new ContactRepository();
        }
        // GET api/values
        public IEnumerable<Contact> Get()
        {
            return repository.All;
        }

        // GET api/values/5
        public Contact Get(int id)
        {
            return repository.GetById(id);
        }

        // POST api/values
        public void Post(Contact value)
        {
            repository.Add(value);
        }

        // PUT api/values/5
        public void Put(int id, string name, string email, string notes)
        {
            Contact contact = repository.GetById(id);
            contact.Name = name;
            contact.Email = email;
            contact.Notes = notes;
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            repository.Delete(id);
        }
    }
}