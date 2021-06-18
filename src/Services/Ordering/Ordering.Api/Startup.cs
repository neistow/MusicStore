using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Api.Application;
using Ordering.Api.Application.Abstract;
using Ordering.Api.Domain.Abstract;
using Ordering.Api.Infrastructure;
using Ordering.Api.IntegrationEventHandlers;
using Shared.Hosting;
using Shared.Hosting.Extensions;

namespace Ordering.Api
{
    public class Startup : BaseStartup
    {
        public Startup(IWebHostEnvironment hostEnvironment, IConfiguration configuration) : base(hostEnvironment, configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddEventBus(opt =>
            {
                opt.ConnectionString = Configuration["RabbitMQ:ConnectionString"];
                opt.SubscriptionPrefixId = Configuration["RabbitMQ:SubscriptionPrefixId"];
                opt.RegistrationAssemblies = new[] {typeof(Startup).Assembly};
            });

            services.AddDbContext<OrderingContext>(opt => opt.UseSqlite(Configuration.GetConnectionString("Database")));

            services.AddAutoMapper(typeof(Startup).Assembly);

            services.AddScoped<IOrderingRepository, OrderingRepository>();
            services.AddScoped<IOrderingService, OrderingService>();
            services.AddScoped<BasketCheckoutEventHandler>();
        }
    }
}