

using Framework.CQRS.Commands;
using Framework.CQRS.Queries;
using System.Threading.Tasks;

namespace Framework.CQRS.Dispatchers
{
    public interface IDispatcher
    {
        Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand;
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
    }
}