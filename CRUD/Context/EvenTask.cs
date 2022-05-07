using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUD.Context
{
    public class EvenTask : ToDoTask
    {
        [Key]
        public int EvenTaskId { get; set; }
        public ICollection<TaskHistory> EvenTaskHistory { get; set; }
    }
}
