using System.ComponentModel;

namespace CRUD.Context
{
    public class ToDoTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
        public int UserId { get; set; }
        [DefaultValue(true)]
        public bool IsActive { get; set; }
    }
}
