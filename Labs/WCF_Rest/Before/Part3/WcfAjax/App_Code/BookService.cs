using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using System.IO;
using System.Web.Hosting;

[ServiceContract(Namespace = "http://develop.com")]
interface IBookService
{
    // TODO: Add [WebGet] attribute
    [OperationContract]
    List<BookTitle> GetBookTitles();

    // TODO: Add [WebGet] attribute
    [OperationContract]
    BookInfo GetBookInfo(string isbn);

    // TODO: Add [WebInvoke] attribute
    [OperationContract]
    void SaveBookInfo(BookInfo book);
}

[ServiceContract(Namespace = "http://develop.com")]
interface IPhotoService
{
    // TODO: Add [WebGet] attribute,
    // with UriTemplate property of "{isbn}"
    [OperationContract]
    Stream GetBookPhoto(string isbn);
}

class BookService : IBookService, IPhotoService
{
    public List<BookTitle> GetBookTitles()
    {
        // Get path to the data source
        string path = string.Format("{0}books.xml",
            HostingEnvironment.ApplicationPhysicalPath);

        // Return book titles
        return BookTitle.GetTitles(path);
    }

    public BookInfo GetBookInfo(string isbn)
    {
        // Throw exception if isbn is not present
        if (isbn == null)
            throw new Exception("Isbn not supplied");

        // Get path to the data source
        string path = string.Format("{0}books.xml",
            HostingEnvironment.ApplicationPhysicalPath);

        // TODO: Tell the browser NOT to cache this object
        //WebOperationContext.Current.OutgoingResponse.Headers["cache-control"] = "no-store";
        
        // Return book
        return BookInfo.GetBook(isbn, path);
    }

    public void SaveBookInfo(BookInfo book)
    {
        // Get path to the data source
        string path = string.Format("{0}books.xml",
            HostingEnvironment.ApplicationPhysicalPath);

        // Save book
        BookInfo.SaveBook(book, path);
    }

    public Stream GetBookPhoto(string isbn)
    {
        // Throw exception if isbn is not present
        if (isbn == null)
            throw new Exception("Isbn not supplied");

        // Get path to the data source
        string path = string.Format("{0}..\\..\\..\\Photos",
            HostingEnvironment.ApplicationPhysicalPath);

        // Return book photo
        return BookInfo.GetBookPhoto(isbn, path);
    }
}

