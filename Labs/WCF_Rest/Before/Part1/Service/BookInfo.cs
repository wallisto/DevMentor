using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Runtime.Serialization;

namespace WcfBindings
{
    [DataContract(Namespace = "http://develop.com")]
    class BookInfo
    {
        [DataMember]
        public string Category { get; set; }
        [DataMember]
        public string Topic { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public string Isbn { get; set; }
        [DataMember]
        public string[] Authors { get; set; }
        [DataMember]
        public string WebSite { get; set; }

        public static BookInfo GetBook(string isbn, string url)
        {
            // Read Xml from url
            XElement xml = XElement.Load(url);

            // Create a query of IEnumerable<BookInfo> from the xml.
            XNamespace ns = "http://develop.com";
            BookInfo book =
                ( from b in xml.Elements(ns + "book")
                where (string)b.Element(ns + "isbn") == isbn
                select new BookInfo
                {
                    Title = (string)b.Element(ns + "title"),
                    Category = (string)b.Element(ns + "category"),
                    Topic = (string)b.Element(ns + "topic"),
                    Price = (decimal)b.Element(ns + "price"),
                    Isbn = (string)b.Element(ns + "isbn"),
                    WebSite = (string)b.Element(ns + "link"),
                    Authors = (from a in b.Element(ns + "authors")
                               .Elements(ns + "author")
                               select (string)a).ToArray()
                }).SingleOrDefault();
            return book;
        }

        public static Stream GetBookPhoto(string isbn, string dir)
        {
            string path = string.Format(@"{0}\{1}.jpg", dir, isbn);
            Stream s = null;
            try
            {
                s = new FileStream(path, FileMode.Open, FileAccess.Read);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (s != null) s.Close();
            }
            return s;
        }
    }
}
