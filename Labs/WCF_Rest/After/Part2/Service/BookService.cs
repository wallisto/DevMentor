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
    [ServiceContract(Namespace = "http://develop.com")]
    [ServiceKnownType(typeof(Rss20FeedFormatter))]
    [ServiceKnownType(typeof(Atom10FeedFormatter))]
    interface IBookService
    {
        [OperationContract]
        [WebGet(UriTemplate = "{format}")]
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

            // Create a feed
            SyndicationFeed feed = new SyndicationFeed();
            feed.Title = new TextSyndicationContent("Best .Net Books");
            feed.Items = feedItems;

            // Return feed formatter for specified format
            SyndicationFeedFormatter formatter = null;
            switch (format)
            {
                case "atom":
                    return new Atom10FeedFormatter(feed);
                case "rss":
                    return new Rss20FeedFormatter(feed);
                default:
                    throw new ArgumentException("Unsupported feed format: " +
                        format);
            }
        }
    }
}
