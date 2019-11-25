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
using TaskMgr.Domain.Commands.Identity;
using TaskMgr.Domain.Entities;
using TaskMgr.Domain.Events;
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
           
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                cors =>
                {
                    cors.WithOrigins("http://localhost:4200")
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
            if (env.IsDevelopment() || env.EnvironmentName == "local")
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(MyAllowSpecificOrigins);

            app.UseErrorHandler();
            app.UseAuthentication();

            app.UseMvc();
            ConfigureEventBus(app);
            AddDefaultUser(app).GetAwaiter();
        }

        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<NewCodeGenerated, IdentityEventsHandler>();

        }

        async Task AddDefaultUser(IApplicationBuilder app)
        {
            var dispatcher = app.ApplicationServices.GetRequiredService<IDispatcher>();
            var cmd = new SignUp(Guid.NewGuid().ToString(), 
                "khaledov@gmail.com", 
                "Password26",
                "Khaled", 
                "Ramadan");
            await dispatcher.SendAsync(cmd);

        }
    }
}
