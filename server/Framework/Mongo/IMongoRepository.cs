using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Framework.Mongo
{
    public interface IMongoRepository<TEntity, T> where TEntity : IIdentity<T>
    {
        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> Get(T id);
        Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        Task<IList<TEntity>> Get();
        Task Add(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(T id);
        Task<bool> Exists(Expression<Func<TEntity, bool>> predicate);
    }
}
