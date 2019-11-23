using System.Threading.Tasks;

namespace Framework.EventBus.Abstractions
{
    public interface IDynamicEventHandler
    {
        Task Handle(dynamic eventData);
    }
}
