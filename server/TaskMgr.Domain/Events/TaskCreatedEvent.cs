using Framework.CQRS.Events;
using System;
using System.Collections.Generic;
using System.Text;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Domain.Events
{
    public class TaskCreatedEvent : IEvent
    {
        public TaskCreatedEvent(string id, string description, DateTime? duedate, PriorityLevel priority)
        {
            Description = description;
            DueDate = duedate;
            Priority = priority;
            Id = id;
            CreationDate = DateTime.UtcNow;
           
        }

        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public PriorityLevel Priority { get; set; }
        public string Id { get; set; }
        

        public DateTime CreationDate { get; set; }
    }
}
