using ToDoModel;
using Repository;

namespace ToDoRepository
{
    public interface IToDoListRepository : IRepository<ToDoList>
    {
        bool CreateToDoList(ToDoList entity);
        void AddToDoList(ToDoList entity);
        ToDoList GetList(int id);
    }

}
