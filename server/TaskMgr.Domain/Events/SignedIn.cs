using Framework.CQRS.Events;
using Newtonsoft.Json;
using System;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Domain.Events
{
    public class SignedIn : IEvent
    {
        public string UserId { get; }
        public string Name { get; }
        public RefreshToken RefreshToken { get; }
        public DateTime CreationDate { get; }


        [JsonConstructor]
        public SignedIn(string userId, string firstName, string lastName, RefreshToken refreshToken)
        {
            UserId = userId;
            Name = firstName + " " + lastName;
            RefreshToken = refreshToken;
            CreationDate = DateTime.UtcNow;
        }
    }
}
