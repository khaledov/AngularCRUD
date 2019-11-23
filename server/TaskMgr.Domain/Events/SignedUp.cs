using Framework.CQRS.Events;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskMgr.Domain.Events
{
    public class SignedUp : IEvent
    {
        public Guid UserId { get; }
        public string Email { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public DateTime CreationDate { get; }
        [JsonConstructor]
        public SignedUp(Guid userId, string email, string firstName, string lastName)
        {
            UserId = userId;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            CreationDate = DateTime.UtcNow;
        }
    }
}
