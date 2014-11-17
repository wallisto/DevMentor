using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.IO;
using System.Runtime.Serialization;
using System.ServiceModel.Web;

namespace WcfBindings
{
    [ServiceContract(Namespace = "http://develop.com")]
    interface IBookService
    {
        // TODO: Add WebGet with UriTemplate = "titles"
        [OperationContract]
        List<BookTitle> GetBookTitles();

        // TODO: Add WebGet with UriTemplate = "books?isbn={isbn}"
        [OperationContract]
        BookInfo GetBookInfo(string isbn);

        // TODO: Add WebGet with UriTemplate = "photos?isbn={isbn}"
        [OperationContract]
        Stream GetBookPhoto(string isbn);
    }

    class BookService : IBookService
    {
        public List<BookTitle> GetBookTitles()
        {
            return BookTitle.GetTitles(@"..\..\books.xml");
        }

        public BookInfo GetBookInfo(string isbn)
        {
            return BookInfo.GetBook(isbn, @"..\..\books.xml");
        }

        public Stream GetBookPhoto(string isbn)
        {
            return BookInfo.GetBookPhoto(isbn, @"..\..\..\..\..\Photos");
        }
    }
}
