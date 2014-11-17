using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Client
{
    class BookTitle
    {
        public string Isbn { get; set; }
        public string Title { get; set; }

        public static List<BookTitle> ParseTitles(string xmlStr)
        {
            // Return if null
            if (xmlStr == null) return null;

            // Parse xml and query for titles
            XElement xml = XElement.Parse(xmlStr);
            XNamespace ns = "http://develop.com";
            IEnumerable<BookTitle> titlesQuery =
                from b in xml.Elements(ns + "BookTitle")
                orderby (string)b.Element(ns + "Title")
                select new BookTitle
                {
                    Title = (string)b.Element(ns + "Title"),
                    Isbn = (string)b.Element(ns + "Isbn"),
                };
            return titlesQuery.ToList();
        }
    }
}
