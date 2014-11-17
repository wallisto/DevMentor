using System.Data.Objects;
using System.Data.SqlClient;
using System.Linq;
using ToDoModel;

namespace ToDoRepository
{
    public class ToDoListRepository : ToDoRepository<ToDoList>, IToDoListRepository
    {
        public ToDoListRepository(string connectionString)
            : base(connectionString)
        {
        }

        public bool CreateToDoList(ToDoList entity)
        {
            if (CanCreate(entity))
            {
                ObjectSet.AddObject(entity);
                Save();
                return true;
            }
            return false;
        }

        protected bool CanCreate(ToDoList entity)
        {
            SqlParameter parameter = new SqlParameter("@1", entity.Name);
            ObjectResult<ToDoList> results = ObjectContext.ExecuteStoreQuery<ToDoList>("select * from ToDoLists where Name=@1", new[] { parameter });
            return results.Count() <= 0;
        }

        public void AddToDoList(ToDoList entity)
        {
            ObjectSet.Attach(entity);
            Save();
        }

        public ToDoList GetList(int id)
        {
            ToDoList list = (from e in Entities
                       where e.Id == id
                       select e).FirstOrDefault();

            return list;
        }
    }
}
