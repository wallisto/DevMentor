using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Runtime.Serialization;

namespace WcfBindings
{
    [DataContract(Namespace = "http://develop.com")]
    class BookTitle
    {
        [DataMember]
        public string Isbn { get; set; }
        [DataMember]
        public string Title { get; set; }

        public static List<BookTitle> GetTitles(string url)
        {
            // Read Xml from url
            XElement xml = XElement.Load(url);

            // Create a query of IEnumerable<Book> from the xml.
            XNamespace ns = "http://develop.com";
            IEnumerable<BookTitle> titlesQuery =
                from b in xml.Elements(ns + "book")
                orderby (string)b.Element(ns + "title")
                select new BookTitle
                {
                    Title = (string)b.Element(ns + "title"),
                    Isbn = (string)b.Element(ns + "isbn"),
                };

            // Return List<Book> by calling ToList on books query
            return titlesQuery.ToList();
        }
    }
}
