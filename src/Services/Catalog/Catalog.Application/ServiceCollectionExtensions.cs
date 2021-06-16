using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Application
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationDi(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ServiceCollectionExtensions).Assembly);
            services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);
        }
    }
}