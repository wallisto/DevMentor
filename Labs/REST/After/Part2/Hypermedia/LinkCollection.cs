using System.Collections.Generic;

namespace Hypermedia
{
    public class LinkCollection : List<Link>, ILinkCollection
    {
        public void Add(string name, string uri)
        {
            Add(new Link{ Name = name, Uri = uri});
        }
    }
}