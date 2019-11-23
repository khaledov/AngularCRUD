using System;
using Newtonsoft.Json;

namespace Framework.CQRS.Events
{
    public class RejectedEvent : IRejectedEvent
    {
        public string Reason { get; }
        public string Code { get; }

        public DateTime CreationDate { get; }

        [JsonConstructor]
        public RejectedEvent(string reason, string code)
        {
            Reason = reason;
            Code = code;
            CreationDate = DateTime.UtcNow;
        }

        public static IRejectedEvent For(string name)
            => new RejectedEvent($"There was an error when executing: " +
                                 $"{name}", $"{name}_error");
    }
}
