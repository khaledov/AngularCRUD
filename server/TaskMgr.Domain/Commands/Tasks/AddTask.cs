using Framework.CQRS.Commands;
using System;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Domain.Commands.Tasks
{
    public class AddTask : ICommand
    {
        public AddTask(string description, DateTime? duedate, PriorityLevel priority)
        {
            Description = description;
            DueDate = duedate;
            Priority = priority;
        }

        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public PriorityLevel Priority { get; set; }
    }
}
