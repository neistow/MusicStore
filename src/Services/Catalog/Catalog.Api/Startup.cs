using Catalog.Api.Grpc;
using Catalog.Api.Middleware;
using Catalog.Application;
using Catalog.Infrastructure;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Hosting;
using Shared.Hosting.Extensions;

namespace Catalog.Api
{
    public class Startup : BaseStartup
    {
        public Startup(IWebHostEnvironment hostEnvironment, IConfiguration configuration) : base(hostEnvironment, configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddApplicationDi();
            services.AddInfrastructureDi(Configuration);

            services.AddValidatorsFromAssembly(typeof(Startup).Assembly);

            services.AddGrpc();

            services.AddEventBus(opt =>
            {
                opt.ConnectionString = Configuration["RabbitMQ:ConnectionString"];
                opt.SubscriptionPrefixId = Configuration["RabbitMQ:SubscriptionPrefixId"];
            });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("GrpcScope", cfg =>
                {
                    cfg.RequireAuthenticatedUser();
                    cfg.RequireClaim("scope", "grpc");
                });

                opt.AddPolicy("AdminScope", cfg =>
                {
                    cfg.RequireAuthenticatedUser();
                    cfg.RequireClaim("scope", "admin");
                });
            });
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            base.Configure(app, env);
        }

        protected override void ConfigureEndpoints(IEndpointRouteBuilder routeBuilder)
        {
            routeBuilder.MapGrpcService<CatalogGrpcService>();
        }
    }
}