using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using Hypermedia;
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
        public IEnumerable<Resource<Contact>> Get(HttpRequestMessage request)
        {
            return repository.All
                             .Select(c => new Resource<Contact>(c, new Dictionary<string,string>
                             {
                                 {"self", request.RequestUri.AbsoluteUri + "/" + c.Id},
                             }));
        }

        // GET api/values/5
        public Resource<Contact> Get(HttpRequestMessage request, int id)
        {
            Contact contact = repository.GetById(id);

            if (contact == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }

            Uri requestUri = request.RequestUri;

            var parentBuilder = new UriBuilder(requestUri.Scheme, requestUri.Host, requestUri.Port, "contacts");

            return new Resource<Contact>(contact, new Dictionary<string,string>
                {
                    {"self", requestUri.AbsoluteUri},
                    {"parent", parentBuilder.Uri.ToString()},
                });
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