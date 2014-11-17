using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.IO;
using System.Runtime.Serialization;
using System.ServiceModel.Web;
using System.ServiceModel.Syndication;

namespace WcfBindings
{
    // TODO: Add two ServiceKnownType attributes,
    // for Rss20FeedFormatter and Atom10FeedFormatter
    [ServiceContract(Namespace = "http://develop.com")]
    interface IBookService
    {
        // TODO: Add a WebGet attribute with a UriTemplate for "{format}"
        [OperationContract]
        SyndicationFeedFormatter GetRssFeed(string format);
    }

    class BookService : IBookService
    {
        public SyndicationFeedFormatter GetRssFeed(string format)
        {
            // Get the books
            List<Book> books = Book.GetBooks((@"..\..\books.xml"));

            // Create feed items
            IEnumerable<SyndicationItem> feedItems =
                from b in books
                select new SyndicationItem
                    (b.Title, b.ToString(), new Uri(b.WebSite));

            // TODO: Create a new SyndicationFeed,
            // then set the Title to a new TextSyndicationContent,
            // and set Items to feedItems

            // TODO: Use a switch block on the format parameter,
            // creating a new Atom10FeedFormatter for "atom",
            // or a new Rss20FeedFormatter for "rss".
            // Pass the feed you just created to the ctor,
            // then return the feed.

            return null; // placeholder
        }
    }
}
