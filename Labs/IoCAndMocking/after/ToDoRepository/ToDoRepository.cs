using System.Linq;
using Repository;
using System.Data.Objects;

namespace ToDoRepository
{
    public class ToDoRepository<T> : IRepository<T> where T : class
    {
        protected ObjectContext ObjectContext;
        protected ObjectSet<T> ObjectSet;


        public ToDoRepository(string connectionString)
        {
            ObjectContext = new ObjectContext(connectionString);
            ObjectSet = ObjectContext.CreateObjectSet<T>();
        }

        public IQueryable<T> Entities
        {
            get { return ObjectSet; }
        }

        public T New()
        {
            return ObjectSet.CreateObject();
        }

        public void Add(T entity)
        {
            ObjectSet.Attach(entity);
        }

        public void Create(T entity)
        {
            ObjectSet.AddObject(entity);
        }

        public void Delete(T entity)
        {
            ObjectSet.DeleteObject(entity);
        }

        public void Save()
        {
            ObjectContext.SaveChanges();
        }

        public void Dispose()
        {
            ObjectContext.Dispose();
        }
    }
}
