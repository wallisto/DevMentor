using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Runtime.Serialization;

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
            (from b in xml.Elements(ns + "book")
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
        if (book == null)
            throw new Exception("Isbn not found: " + isbn);
        return book;
    }

    public static void SaveBook(BookInfo book, string url)
    {
        // Read Xml from url
        XElement xml = XElement.Load(url);

        // Get existing book
        XNamespace ns = "http://develop.com";
        XElement bookXml =
            ( from b in xml.Elements(ns + "book")
            where (string)b.Element(ns + "isbn") == book.Isbn
            select b ).SingleOrDefault();

        if (bookXml == null)
            throw new Exception("Isbn not found: " + book.Isbn);

        // Change the book
        bookXml.Element(ns + "category").SetValue(book.Category);
        bookXml.Element(ns + "topic").SetValue(book.Topic);
        bookXml.Element(ns + "price").SetValue(book.Price);

        // Save the xml
        xml.Save(url);
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
