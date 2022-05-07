using CRUD.Models;
using System.Collections.Generic;

namespace CRUD.Service
{
    public interface IToDoService
    {
        IEnumerable<ToDoTaskViewModel> GetAllTaskAsync();
        ToDoTaskViewModel GetTaskById(int taskId);
        bool UpdateTask(ToDoTaskViewModel task);
    }
}
