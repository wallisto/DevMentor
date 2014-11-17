using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;
using Hypermedia;
using Repository;

namespace Contacts.Formatters
{
    public class ContactResourceFormatter : MediaTypeFormatter
    {
        public ContactResourceFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/xml"));
        }
        public override bool CanReadType(Type type)
        {
            throw new NotImplementedException();
        }

        public override bool CanWriteType(Type type)
        {
            return typeof(IEnumerable<Resource<Contact>>).IsAssignableFrom(type) || type == typeof(Resource<Contact>);
        }

        public override Task WriteToStreamAsync(Type type, object value, System.IO.Stream stream, HttpContentHeaders contentHeaders, System.Net.TransportContext transportContext)
        {
            return Task.Run(() =>
                {
                    if (typeof(IEnumerable<Resource<Contact>>).IsAssignableFrom(type))
                    {
                        WriteToStream(stream, (IEnumerable<Resource<Contact>>)value);
                    }
                    else if (type == typeof(Resource<Contact>))
                    { 
                        WriteToStream(stream, (Resource<Contact>)value);
                    }
                });
        }

        private void WriteToStream(Stream stream, IEnumerable<Resource<Contact>> enumerable)
        {
            var xml = new XElement("Contacts",
                from c in enumerable
                select CreateResourceXml(c));

            xml.Save(stream);
        }

        private void WriteToStream(Stream stream, Resource<Contact> resource)
        {
            var xml = CreateResourceXml(resource);

            xml.Save(stream);
        }

        private static XElement CreateResourceXml(Resource<Contact> resource)
        {
            return new XElement("Contact",
                            new XElement("Id", resource.ResourceValue.Id),
                            new XElement("Name", resource.ResourceValue.Name),
                            new XElement("Email", resource.ResourceValue.Email),
                            new XElement("Notes", resource.ResourceValue.Notes),
                            from l in resource.Links
                            select new XElement("Link",
                                new XAttribute("Name", l.Name),
                                new XAttribute("Uri", l.Uri)));
        }
    }
}