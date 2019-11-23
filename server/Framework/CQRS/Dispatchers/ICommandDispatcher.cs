
using Framework.CQRS.Commands;
using System.Threading.Tasks;


namespace Framework.CQRS.Dispatchers
{
    public interface ICommandDispatcher
    {
         Task SendAsync<T>(T command) where T : ICommand;
    }
}