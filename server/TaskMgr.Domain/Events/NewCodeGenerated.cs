using Framework.CQRS.Events;
using Newtonsoft.Json;
using System;

namespace TaskMgr.Domain.Events
{
    public class NewCodeGenerated : IEvent
    {
        public Guid UserId { get; }
        public string Email { get; }
        public string RandomPassword { get; }

        public DateTime CreationDate { get; }

        [JsonConstructor]
        public NewCodeGenerated(Guid userId, string email, string randomPassword)
        {
            UserId = userId;
            Email = email;
            RandomPassword = randomPassword;
            CreationDate = DateTime.UtcNow;
        }
    }
}
