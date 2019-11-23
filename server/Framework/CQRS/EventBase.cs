using System;

namespace Framework.CQRS
{
    public class EventBase : Message
    {
        public DateTime TimeStamp { get; private set; }

        public EventBase()
        {
            TimeStamp = DateTime.Now;
            Name = this.GetType().Name;
        }
    }
}