using Framework.CQRS.Events;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskMgr.Domain.Events
{
    public class PasswordChanged : IEvent
    {
        public Guid UserId { get; }
        public DateTime CreationDate { get; }
        [JsonConstructor]
        public PasswordChanged(Guid userId)
        {
            UserId = userId;
            CreationDate = DateTime.UtcNow;
        }
    }
}
