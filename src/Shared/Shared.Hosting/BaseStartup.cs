using System;
using System.Globalization;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSwag;
using NSwag.Generation.Processors.Security;
using Shared.Hosting.Extensions;
using Shared.Hosting.Options;

namespace Shared.Hosting
{
    public class BaseStartup
    {
        public IWebHostEnvironment HostEnvironment { get; }
        public IConfiguration Configuration { get; }

        public BaseStartup(IWebHostEnvironment hostEnvironment, IConfiguration configuration)
        {
            HostEnvironment = hostEnvironment;
            Configuration = configuration;
        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddFluentValidation(o =>
            {
                o.DisableDataAnnotationsValidation = true;
                o.ValidatorOptions.LanguageManager.Enabled = false;
            });

            services.AddCurrentUser();

            var corsSection = Configuration.GetSection("Cors");
            services.Configure<CorsSettings>(corsSection);
            services.BuildCors(corsSection.Get<CorsSettings>());

            var jwtSection = Configuration.GetSection("Jwt");
            services.Configure<JwtSettings>(jwtSection);
            services.AddAuth(jwtSection.Get<JwtSettings>());

            services.AddOpenApiDocument(document =>
            {
                document.AddSecurity("Bearer", Array.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "JWT",
                    Description = "Your token:"
                });

                document.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("Bearer"));
            });
        }

        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(CultureInfo.InvariantCulture)
            });

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseRouting();
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                ConfigureEndpoints(endpoints);
                endpoints.MapGet("/ping", async context => { await context.Response.WriteAsync("Pong"); });
            });
        }

        protected virtual void ConfigureEndpoints(IEndpointRouteBuilder routeBuilder)
        {
        }
    }
}