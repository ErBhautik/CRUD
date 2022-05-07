using AutoMapper;
using CRUD.Context;
using CRUD.HttpHelper;
using CRUD.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRUD.Service
{
    public class ToDoService : IToDoService
    {
        private readonly CrudDbContext _context;
        private readonly IAPIHttpHelper _apiHttpHelper;
        private readonly IMapper _mapper;
        public ToDoService(CrudDbContext context, IAPIHttpHelper apiHttpHelper, IMapper mapper)
        {
            _context = context;
            _apiHttpHelper = apiHttpHelper;
            _mapper = mapper;
        }
        public IEnumerable<ToDoTaskViewModel> GetAllTaskAsync()
        {
            try
            {
                var response = _apiHttpHelper.GetRequest<List<ToDoTaskAPI>>("https://jsonplaceholder.typicode.com/todos");
                var oddTasks = response.Result.Where(item => item.Id % 2 != 0).
                    Select(x => new OddTask
                    {
                        Id = x.Id,
                        IsActive = true,
                        IsCompleted = x.Completed,
                        Title = x.Title,
                        UserId = x.UserId,
                    }).ToList();

                var evenTasks = response.Result.Where(item => item.Id % 2 == 0).
                    Select(x => new EvenTask
                    {
                        Id = x.Id,
                        IsActive = true,
                        IsCompleted = x.Completed,
                        Title = x.Title,
                        UserId = x.UserId,
                    }).ToList();
                var existingOddTasks = GetAllOddTasks();
                if (!existingOddTasks.Any() && oddTasks.Any())
                {
                    _context.OddTask.AddRange(oddTasks);
                    _context.SaveChanges();
                    existingOddTasks = GetAllOddTasks();
                }
                var existingEvenTasks = GetAllEvenTasks();
                if (!existingEvenTasks.Any() && evenTasks.Any())
                {
                    _context.EvenTask.AddRange(evenTasks);
                    _context.SaveChanges();
                    existingOddTasks = GetAllOddTasks();
                }
                var getAllTask = existingOddTasks.Select(x => new ToDoTaskViewModel()
                {
                    Id = x.Id,
                    IsActive = x.IsActive,
                    IsCompleted = x.IsCompleted,
                    IsEdited = x.OddTaskHistory.Any(),
                    TaskId = x.OddTaskId,
                    Title = x.Title,
                    UserId = x.UserId
                }).ToList().Concat
                (
                    existingEvenTasks.Select(x => new ToDoTaskViewModel()
                    {
                        Id = x.Id,
                        IsActive = x.IsActive,
                        IsCompleted = x.IsCompleted,
                        IsEdited = x.EvenTaskHistory.Any(),
                        TaskId = x.EvenTaskId,
                        Title = x.Title,
                        UserId = x.UserId
                    }
                )).OrderBy(x => x.UserId).ToList();
                return getAllTask;
            }
            catch (Exception ex)
            {
                return new List<ToDoTaskViewModel>();
            }
        }

        public ToDoTaskViewModel GetTaskById(int taskId)
        {
            if (taskId > 0)
            {
                if (taskId % 2 != 0)
                {
                    return _context.OddTask.Where(x => x.OddTaskId == taskId)
                        .Select(x => new ToDoTaskViewModel()
                        {
                            Id = x.Id,
                            IsActive = x.IsActive,
                            IsCompleted = x.IsCompleted,
                            TaskId = x.OddTaskId,
                            Title = x.Title,
                            UserId = x.UserId
                        }).FirstOrDefault();
                }
                else
                {
                    return _context.EvenTask.Where(x => x.EvenTaskId == taskId)
                        .Select(x => new ToDoTaskViewModel()
                        {
                            Id = x.Id,
                            IsActive = x.IsActive,
                            IsCompleted = x.IsCompleted,
                            TaskId = x.EvenTaskId,
                            Title = x.Title,
                            UserId = x.UserId
                        }).FirstOrDefault();
                }
            }
            else
            {
                return null;
            }
        }
        public bool UpdateTask(ToDoTaskViewModel task)
        {
            if (task.TaskId % 2 != 0)
            {
                var oddTask = _context.OddTask.Where(x => x.OddTaskId == task.TaskId).AsNoTracking().FirstOrDefault();
                if (ChangedFields(task, _mapper.Map<ToDoTaskViewModel>(oddTask)).Any())
                {
                    var taskHistory = _mapper.Map<TaskHistory>(oddTask);
                    _context.TaskHistory.Add(taskHistory);
                    _context.SaveChanges();
                    oddTask = _mapper.Map<OddTask>(task);
                    _context.OddTask.Update(oddTask);
                    _context.SaveChanges();
                }
            }
            else
            {
                var eventTask = _context.EvenTask.Where(x => x.EvenTaskId == task.TaskId).AsNoTracking().FirstOrDefault();
                if (ChangedFields(task, _mapper.Map<ToDoTaskViewModel>(eventTask)).Any())
                {
                    var taskHistory = _mapper.Map<TaskHistory>(eventTask);
                    _context.TaskHistory.Add(taskHistory);
                    _context.SaveChanges();
                    eventTask = _mapper.Map<EvenTask>(task);
                    _context.EvenTask.Update(eventTask);
                    _context.SaveChanges();
                }
            }
            return true;
        }
        private IEnumerable<EvenTask> GetAllEvenTasks()
        { 
            return _context.EvenTask.Include(x => x.EvenTaskHistory).ToList();
        }
        private IEnumerable<OddTask> GetAllOddTasks()
        {
            return _context.OddTask.Include(x => x.OddTaskHistory).ToList();
        }
        public IEnumerable<string> ChangedFields<T>(T first, T second)
        {
            if (first.GetType() != second.GetType())
                throw new ArgumentOutOfRangeException("Objects should be of the same type");

            var properties = first
                .GetType()
                .GetProperties();

            foreach (var property in properties)
            {
                if (!object.Equals(property.GetValue(first), property.GetValue(second)))
                {
                    yield return property.Name;
                }
            }
        }
    }
}
