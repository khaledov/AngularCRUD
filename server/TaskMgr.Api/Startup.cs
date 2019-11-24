using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Framework.Authentication;
using Framework.CQRS.Dispatchers;
using Framework.EventBus;
using Framework.EventBus.Abstractions;
using Framework.Mongo;
using Framework.MVC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TaskMgr.Api.EventHandlers;
using TaskMgr.Api.Services;
using TaskMgr.Api.Settings;
using TaskMgr.Domain;
using TaskMgr.Domain.Entities;
using TaskMgr.Domain.Services;

namespace TaskMgr.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public IConfiguration Configuration { get; }
        public IContainer Container { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddTaskMgrMvc();

            services.AddJwt();
            services.AddSingleton<IClaimsProvider, ClaimsProvider>();
            services.AddTransient<IdentityEventsHandler>();
            services.AddAuthorization(x =>
            x.AddPolicy("admin", p => p.RequireRole("admin")));
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                cors =>
                {
                    cors.WithOrigins("http://localhost:4200", "http://localhost:4204")
                                        .AllowAnyHeader()
                                        .AllowAnyMethod();
                });
            });
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly(), Assembly.Load("ITP.Framework"), Assembly.Load("MsgMgr.Domain"))
                    .AsImplementedInterfaces();
            builder.Populate(services);
            builder.AddMongo();
            builder.RegisterRepositories();
            builder.AddDispatchers();
            builder.AddCQRS();
            builder.RegisterType<InMemoryEventBus>().As<IEventBus>();

            builder.RegisterType<PasswordHasher<User>>().As<IPasswordHasher<User>>();

            Container = builder.Build();

            return new AutofacServiceProvider(Container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
