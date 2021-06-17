using Catalog.Api.Grpc;
using Catalog.Application;
using Catalog.Infrastructure;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Hosting;

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
        }

        protected override void ConfigureEndpoints(IEndpointRouteBuilder routeBuilder)
        {
            routeBuilder.MapGrpcService<CatalogGrpcService>();
        }
    }
}