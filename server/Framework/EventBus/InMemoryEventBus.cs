using Autofac;
using Framework.CQRS.Events;
using Framework.EventBus.Abstractions;
using System;
using System.Threading.Tasks;

namespace Framework.EventBus
{
    public class InMemoryEventBus : IEventBus
    {
        IEventBusSubscriptionsManager _eventBusSubscriptionsManager;
        private readonly IComponentContext _context;

         public InMemoryEventBus(IEventBusSubscriptionsManager eventBusSubscriptionsManager, IComponentContext context)
        {
            _eventBusSubscriptionsManager = eventBusSubscriptionsManager;
            _context = context;
        }
       

        public async Task Publish<T>(T @event) where T : IEvent
        {
            try
            {
                var handlersInfo = _eventBusSubscriptionsManager.GetHandlersForEvent<T>();
                
                foreach (var info in handlersInfo)
                {
                    var handler = _context.Resolve(info.HandlerType) as IEventHandler<T>;
                    if(handler!=null)
                    await handler.HandleAsync(@event);


                }
            }
            catch (Exception ex)
            {

            }
           
        }

        public void Subscribe<T, TH>()
            where T : IEvent
            where TH : IEventHandler<T>
        {
            _eventBusSubscriptionsManager.AddSubscription<T, TH>();
        }

        public void SubscribeDynamic<TH>(string eventName) where TH : IDynamicEventHandler
        {
            _eventBusSubscriptionsManager.AddDynamicSubscription<TH>(eventName);
        }

        public void Unsubscribe<T, TH>()
            where T : IEvent
            where TH : IEventHandler<T>
        {
            _eventBusSubscriptionsManager.RemoveSubscription<T, TH>();
        }

        public void UnsubscribeDynamic<TH>(string eventName) where TH : IDynamicEventHandler
        {
            _eventBusSubscriptionsManager.RemoveDynamicSubscription<TH>(eventName);
        }


      
    }
}
