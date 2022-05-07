namespace CRUD.Context
{
    public class TaskHistory
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
        public int UserId { get; set; }
        public int? EvenTaskId { get; set; }
        public EvenTask EvenTask { get; set; }
        public int? OddTaskId { get; set; }
        public OddTask OddTask { get; set; }
    }
}
