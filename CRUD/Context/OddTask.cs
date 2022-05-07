using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUD.Context
{
    public class OddTask : ToDoTask
    {
        [Key]
        public int OddTaskId { get; set; }
        public ICollection<TaskHistory> OddTaskHistory { get; set; }
    }
}
