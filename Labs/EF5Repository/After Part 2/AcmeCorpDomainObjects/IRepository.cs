using System.Linq;

namespace AcmeCorpDomainObjects
{
    public interface IRepository<T>
    {
        IQueryable<T> Entities { get; }

        void Add(T entity);
        void Remove(T entity);
    }
}