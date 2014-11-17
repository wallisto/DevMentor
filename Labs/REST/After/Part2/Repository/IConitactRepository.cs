using System;

namespace Repository
{
    public interface IContactRepository
    {
        void Add(Contact contact);
        System.Collections.Generic.IEnumerable<Contact> All { get; }
        void Delete(int id);
        Contact GetById(int id);
    }
}
