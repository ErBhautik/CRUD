using CRUD.Models;
using CRUD.Service;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Controllers
{
    public class Task : Controller
    {
        private readonly IToDoService _toDoService;
        public Task(IToDoService toDoService)
        {
            _toDoService = toDoService;
        }
        public IActionResult Index()
        {
            return View(_toDoService.GetAllTaskAsync());
        }
        public IActionResult Edit(int TaskId)
        {
            return View(_toDoService.GetTaskById(TaskId));
        }
        [HttpPost]
        public IActionResult Edit(ToDoTaskViewModel task)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                if(_toDoService.UpdateTask(task))
                    return RedirectToAction("Index");

                return View();
            }
        }
    }
}
