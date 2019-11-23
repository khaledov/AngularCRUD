
using System.Threading.Tasks;

namespace Framework.CQRS.Events
{
    public interface IEventHandler<in TEvent> where TEvent : IEvent
    {
        Task HandleAsync(TEvent @event);
    }
}
