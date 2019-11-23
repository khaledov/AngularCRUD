using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskMgr.Persistence.Model
{
    public class TodoItem
    {
        public TodoItem()
        {
            Description = String.Empty;
            DueDate = DateTime.Today.Date;
            Priority = PriorityLevel.Normal;
            State = TaskStatus.Pending;
        }

        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public PriorityLevel Priority { get; set; }
        public TaskStatus State { get; set; }
    }
}
