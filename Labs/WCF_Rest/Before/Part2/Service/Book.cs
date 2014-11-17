using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Runtime.Serialization;

namespace WcfBindings
{
    public class Book
    {
        // Book properties
        public string Category { get; set; }
        public string Topic { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string[] Authors { get; set; }
        public string WebSite { get; set; }

        // Book description
        public override string ToString()
        {
            string output = string.Format("<b>{0}</b> by {1}",
                Title, string.Join(", ", this.Authors));
            output += string.Format("<br/>{0}<br/>{1}<br/><b>{2}</b>",
                Category, Topic, Price.ToString("c"));
            return output;
        }

        public static List<Book> GetBooks(string url)
        {
            // Read Xml from url
            XElement xml = XElement.Load(url);

            // Create a query of IEnumerable<Book> from the xml.
            // Hint: Create XNamespace for http://develop.com,
            // then pre-append it to each local name.
            XNamespace ns = "http://develop.com";
            IEnumerable<Book> booksQuery =
                from b in xml.Elements(ns + "book")
                select new Book
                {
                    Title = (string)b.Element(ns + "title"),
                    Category = (string)b.Element(ns + "category"),
                    Topic = (string)b.Element(ns + "topic"),
                    Price = (decimal)b.Element(ns + "price"),
                    WebSite = (string)b.Element(ns + "link"),
                    Authors = (from a in b.Element(ns + "authors")
                                   .Elements(ns + "author")
                               select (string)a).ToArray()
                };

            // Return List<Book> by calling ToList on books query
            return booksQuery.ToList();
        }
    }
}
