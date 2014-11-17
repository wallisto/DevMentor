using System.Collections.Generic;

namespace Hypermedia
{
    public class Resource<T>
    {
        public Resource(T item)
            : this(item, new Dictionary<string, string>())
        {
        }

        public Resource(T item, Dictionary<string,string> links )
        {
            ResourceValue = item;
            Links = new LinkCollection();

            foreach (string key in links.Keys)
            {
                Links.Add(key, links[key]);
            }
        }

        public T ResourceValue { get; private set; }

        public ILinkCollection Links { get; private set; }
    }
}