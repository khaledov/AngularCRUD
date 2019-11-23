using System;

namespace Framework.CQRS.Events
{
    public interface IEvent : IMessage
    {
        DateTime CreationDate { get; }
    }
}
