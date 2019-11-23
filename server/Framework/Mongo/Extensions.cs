using Autofac;
using Framework.Authentication;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Framework.Mongo
{
    public static class Extensions
    {
        public static void AddMongo(this ContainerBuilder builder)
        {
            builder.Register(context =>
            {
                var configuration = context.Resolve<IConfiguration>();
                var options = configuration.GetOptions<MongoDbOptions>("mongo");

                return options;
            }).SingleInstance();

            builder.Register(context =>
            {
                var options = context.Resolve<MongoDbOptions>();

                return new MongoClient(options.ConnectionString);
            }).SingleInstance();

            builder.Register(context =>
            {
                var options = context.Resolve<MongoDbOptions>();
                var client = context.Resolve<MongoClient>();
                return client.GetDatabase(options.Database);

            }).InstancePerLifetimeScope();


        }

        public static void AddMongoRepository<TEntity, T>(this ContainerBuilder builder, string collectionName)
            where TEntity : IIdentity<T>
            => builder.Register(ctx => new MongoRepository<TEntity, T>(ctx.Resolve<IMongoDatabase>(), collectionName))
                .As<IMongoRepository<TEntity, T>>()
                .InstancePerLifetimeScope();
    }
}
