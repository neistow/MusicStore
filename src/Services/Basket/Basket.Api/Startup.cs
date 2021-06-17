using System;
using Basket.Api.Application;
using Basket.Api.Domain;
using Basket.Api.Infrastructure;
using FluentValidation;
using GrpcCatalog;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Hosting;

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

            services.AddAutoMapper(typeof(Startup).Assembly);

            services.AddGrpcClient<Catalog.CatalogClient>(o =>
            {
                o.Address = new Uri(Configuration["Grpc:CatalogServiceUrl"]);
            });
        }
    }
}