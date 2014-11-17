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
        [OperationContract]
        [WebGet(UriTemplate="titles")]
        List<BookTitle> GetBookTitles();

        [OperationContract]
        [WebGet(UriTemplate = "books?isbn={isbn}")]
        BookInfo GetBookInfo(string isbn);

        [OperationContract]
        [WebGet(UriTemplate = "photos?isbn={isbn}")]
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
