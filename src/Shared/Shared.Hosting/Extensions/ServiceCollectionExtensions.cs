using System;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Shared.Abstractions;
using Shared.Hosting.Infrastructure;
using Shared.Hosting.Infrastructure.EventBus;
using Shared.Hosting.Options;

namespace Shared.Hosting.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void BuildCors(this IServiceCollection services, CorsSettings corsSettings)
        {
            services.AddCors(o =>
            {
                o.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins(corsSettings.AllowedOrigins)
                        .WithMethods(corsSettings.AllowedMethods)
                        .WithHeaders(corsSettings.AllowedHeaders);
                });
            });
        }

        public static void AddAuth(this IServiceCollection services, JwtSettings jwtSettings)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    o.Authority = jwtSettings.Authority;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtSettings.ValidIssuer,
                        ValidateAudience = false,
                        ValidAudience = jwtSettings.ValidAudience,
                        ValidateLifetime = true
                    };
                });
        }

        public static void AddCurrentUser(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddTransient<ICurrentUser, CurrentUser>();
        }
        
        public static void AddEventBus(this IServiceCollection services, Action<EventBusSettings> options)
        {
            var busOptions = new EventBusSettings();

            options.Invoke(busOptions);

            services.AddSingleton(busOptions);
            services.AddSingleton(_ => RabbitHutch.CreateBus(busOptions.ConnectionString));
            services.AddSingleton<MicrosoftDiMessageDispatcher>();
            services.AddSingleton(sp =>
            {
                var subscriber = new AutoSubscriber(sp.GetRequiredService<IBus>(), busOptions.SubscriptionPrefixId)
                {
                    AutoSubscriberMessageDispatcher = sp.GetRequiredService<MicrosoftDiMessageDispatcher>()
                };

                return subscriber;
            });
        }
    }
}