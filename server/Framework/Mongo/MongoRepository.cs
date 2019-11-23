using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Mongo
{
    public class MongoRepository<TEntity, T> : IMongoRepository<TEntity, T> where TEntity : IIdentity<T>
    {
        IMongoCollection<TEntity> Collection { get; }
        public MongoRepository(IMongoDatabase database, string collectionName)
        {
            Collection = database.GetCollection<TEntity>(collectionName);

        }
       

        public async Task<bool> Exists(Expression<Func<TEntity, bool>> predicate)
        => await Collection.Find(predicate).AnyAsync();

        public async Task<TEntity> Get(T id)
        => await Get(e => e.Id.Equals(id));

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        => await Collection.Find(predicate).SingleOrDefaultAsync();

        public async Task<IList<TEntity>> Get()
        => await Collection.AsQueryable().ToListAsync();

        public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
           => await Collection.Find(predicate).ToListAsync();

        public async Task Add(TEntity entity)
       => await Collection.InsertOneAsync(entity);
        public async Task Update(TEntity entity)
       => await Collection.ReplaceOneAsync(e => e.Id.Equals(entity.Id), entity);
        public async Task Delete(T id)
       => await Collection.DeleteOneAsync(e => e.Id.Equals(id));
       
    }
}
