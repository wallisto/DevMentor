using System.Collections.Generic;

namespace Services
{
    public interface IContactRepository
    {
        IEnumerable<Contact> GetAll();
        void Add(Contact contact);
        void Remove(Contact contact);
        void Commit();
    }
}