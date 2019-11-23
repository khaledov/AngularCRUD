
using Framework.CQRS.Events;
using System.Threading.Tasks;

namespace Framework.EventBus.Abstractions
{
    public interface IEventBus
    {
        void Subscribe<T, TH>()
            where T : IEvent
            where TH : IEventHandler<T>;
        void SubscribeDynamic<TH>(string eventName)
            where TH : IDynamicEventHandler;

        void UnsubscribeDynamic<TH>(string eventName)
            where TH : IDynamicEventHandler;

        void Unsubscribe<T, TH>()
            where TH : IEventHandler<T>
            where T : IEvent;

        Task Publish<T>(T @event) where T:IEvent;
    }
}
