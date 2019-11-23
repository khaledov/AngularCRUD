

using Framework.CQRS.Events;
using Framework.EventBus.Abstractions;
using System;
using System.Collections.Generic;


namespace Framework.EventBus
{
    public interface IEventBusSubscriptionsManager
    {
        bool IsEmpty { get; }
        event EventHandler<string> OnEventRemoved;
        void AddDynamicSubscription<TH>(string eventName)
           where TH : IDynamicEventHandler;

        void AddSubscription<T, TH>()
           where T : IEvent
           where TH : IEventHandler<T>;

        void RemoveSubscription<T, TH>()
             where TH : IEventHandler<T>
             where T : IEvent;
        void RemoveDynamicSubscription<TH>(string eventName)
            where TH : IDynamicEventHandler;

        bool HasSubscriptionsForEvent<T>() where T : IEvent;
        bool HasSubscriptionsForEvent(string eventName);
        Type GetEventTypeByName(string eventName);
        void Clear();
        IEnumerable<InMemoryEventBusSubscriptionsManager.SubscriptionInfo> GetHandlersForEvent<T>() where T : IEvent;
        IEnumerable<InMemoryEventBusSubscriptionsManager.SubscriptionInfo> GetHandlersForEvent(string eventName);
        string GetEventKey<T>();
    }
}