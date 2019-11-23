﻿using Framework.Mongo;
using System;

namespace TaskMgr.Domain.Entities
{
    public class TodoItem:IIdentity<int>
    {
        public TodoItem()
        {
            Description = String.Empty;
            DueDate = DateTime.Today.Date;
            Priority = PriorityLevel.Normal;
            State = TaskStatus.Pending;
        }

        
        public int Id { get; set; }

        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public PriorityLevel Priority { get; set; }
        public TaskStatus State { get; set; }
    }
}
