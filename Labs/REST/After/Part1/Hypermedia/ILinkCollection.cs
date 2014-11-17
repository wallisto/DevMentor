using System.Collections.Generic;

namespace Hypermedia
{
    public interface ILinkCollection : IEnumerable<Link>
    {
        void Add(string name, string uri);
    }
}