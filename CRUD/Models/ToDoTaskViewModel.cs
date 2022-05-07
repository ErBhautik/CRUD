using System.ComponentModel.DataAnnotations;

namespace CRUD.Models
{
    public class ToDoTaskViewModel
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        [Required(ErrorMessage = "Title Id is Required")]
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
        [Required(ErrorMessage = "User Id is Required")]
        public int UserId { get; set; }
        public bool IsActive { get; set; }
        public bool IsEdited { get; set; }
    }
}
