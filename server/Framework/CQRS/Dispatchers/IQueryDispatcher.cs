
using Framework.CQRS.Queries;
using System.Threading.Tasks;


namespace Framework.CQRS.Dispatchers
{
    public interface IQueryDispatcher
    {
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
    }
}