using Catalog.Domain.Repositories;
using Catalog.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureDi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CatalogContext>(opt => opt.UseSqlite(configuration.GetConnectionString("Database")));
            services.AddScoped<ICatalogRepository, CatalogRepository>();
        }
    }
}