using Autofac;
using Framework.CQRS.Commands;
using Framework.CQRS.Queries;
using Framework.Mongo;
using System;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Domain
{
    public static class Extensions
    {
        public static void AddCQRS(this ContainerBuilder builder)
        {
            builder.RegisterTypes(typeof(IQuery));
            builder.RegisterType(typeof(IQueryHandler<,>));
            builder.RegisterType(typeof(ICommandHandler<>));
            builder.RegisterTypes(typeof(ICommand));
        }
        public static void RegisterRepositories(this ContainerBuilder builder)
        {

            builder.AddMongoRepository<RefreshToken, Guid>("RefreshTokens");
            builder.AddMongoRepository<User, Guid>("Users");
            builder.AddMongoRepository<TodoItem, int>("TodoItems");
        }

    }
}
