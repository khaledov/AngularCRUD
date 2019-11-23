using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


namespace Framework.Authentication
{
    public static class Extensions
    {
        private static readonly string SectionName = "jwt";

        public static void AddJwt(this IServiceCollection services)
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }
            var section = configuration.GetSection(SectionName);
            var options = configuration.GetOptions<JwtOptions>(SectionName);
            services.Configure<JwtOptions>(section);
            services.AddSingleton(options);
            services.AddSingleton<IJwtHandler, JwtHandler>();
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(jwtOptions =>             {
                //Set default Authentication Schema as Bearer
                jwtOptions.DefaultAuthenticateScheme =
                           JwtBearerDefaults.AuthenticationScheme;                 jwtOptions.DefaultScheme =
                           JwtBearerDefaults.AuthenticationScheme;                 jwtOptions.DefaultChallengeScheme =
                           JwtBearerDefaults.AuthenticationScheme;             })
                .AddJwtBearer(cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey)),
                        ValidIssuer = options.Issuer,
                        ValidAudience = options.ValidAudience,
                        ValidateAudience = options.ValidateAudience,
                        ValidateLifetime = options.ValidateLifetime
                    };
                });

        }



        public static long ToTimestamp(this DateTime dateTime)
        {
            var centuryBegin = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var expectedDate = dateTime.Subtract(new TimeSpan(centuryBegin.Ticks));

            return expectedDate.Ticks / 10000;
        }
        public static TModel GetOptions<TModel>(this IConfiguration configuration, string section) where TModel : new()
        {
            var model = new TModel();
            configuration.GetSection(section).Bind(model);

            return model;
        }

    }
}
