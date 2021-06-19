using System;
using System.Net.Http;
using System.Threading.Tasks;
using Basket.Api.Application;
using Basket.Api.Application.Abstract;
using Basket.Api.Domain.Abstract;
using Basket.Api.Infrastructure;
using Basket.Api.IntegrationEventHandlers;
using Basket.Api.Middleware;
using FluentValidation;
using Grpc.Core;
using IdentityModel.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Hosting;
using Shared.Hosting.Extensions;

namespace Basket.Api
{
    public class Startup : BaseStartup
    {
        public Startup(IWebHostEnvironment hostEnvironment, IConfiguration configuration) : base(hostEnvironment, configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddDbContext<BasketContext>(opt => opt.UseSqlite(Configuration.GetConnectionString("Database")));

            services.AddMemoryCache();

            services.AddValidatorsFromAssembly(typeof(Startup).Assembly);

            services.AddScoped<IBasketRepository, BasketRepository>();

            services.AddScoped<IBasketService, BasketService>();

            services.AddSingleton<IGrpcAuthService, GrpcAuthService>();

            services.AddAutoMapper(typeof(Startup).Assembly);

            services.AddHttpClient();

            services.AddMemoryCache();

            services.AddGrpcClient<GrpcCatalogClient.Catalog.CatalogClient>(opt =>
            {
                opt.Address = new Uri(Configuration["Grpc:CatalogServiceUrl"]);
            }).ConfigureChannel(async (sp, opt) =>
                {
                    var authService = sp.GetRequiredService<IGrpcAuthService>();
                    var token = await authService.GetToken();
                
                    var credentials = CallCredentials.FromInterceptor((ctx, meta) =>
                    {
                        meta.Add("Authorization", $"Bearer {token}");
                        return Task.CompletedTask;
                    });
                
                    opt.Credentials = ChannelCredentials.Create(new SslCredentials(), credentials);
                });

            services.AddEventBus(opt =>
            {
                opt.ConnectionString = Configuration["RabbitMQ:ConnectionString"];
                opt.SubscriptionPrefixId = Configuration["RabbitMQ:SubscriptionPrefixId"];
                opt.RegistrationAssemblies = new[] {typeof(Startup).Assembly};
            });

            services.AddScoped<ItemDeletedEventHandler>();
            services.AddScoped<ItemPriceChangedEventHandler>();
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            base.Configure(app, env);
        }
    }
}