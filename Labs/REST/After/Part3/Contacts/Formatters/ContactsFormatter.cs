using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;
using Repository;

namespace Contacts.Formatters
{
    public class ContactsFormatter : MediaTypeFormatter
    {
        public ContactsFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/xml"));
        }

        public override bool CanReadType(Type type)
        {
            throw new NotImplementedException();
        }

        public override bool CanWriteType(Type type)
        {
            return typeof(IEnumerable<Contact>).IsAssignableFrom(type) || type == typeof(Contact);
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream stream, HttpContentHeaders contentHeaders, TransportContext transportContext)
        {
            return Task.Run(() =>
                {
                    if (typeof(IEnumerable<Contact>).IsAssignableFrom(type))
                    {
                        IEnumerable<Contact> contacts = (IEnumerable<Contact>)value;
                        var formattedContacts = new XElement("Contacts",
                            from c in contacts
                            select CreateContactXml(c));

                        formattedContacts.Save(stream);
                    }
                    else if (type == typeof(Contact))
                    {
                        XElement contactXml = CreateContactXml((Contact)value);
                        contactXml.Save(stream);
                    }
                });
        }

        private XElement CreateContactXml(Contact contact)
        {
            return new XElement("Contact",
                            new XElement("Id", contact.Id),
                            new XElement("Name", contact.Name),
                            new XElement("Email", contact.Email),
                            new XElement("Notes", contact.Notes));
        }
    }
}