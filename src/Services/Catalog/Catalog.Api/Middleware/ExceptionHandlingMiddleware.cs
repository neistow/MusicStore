using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Shared.Hosting.Infrastructure;

namespace Catalog.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            switch (exception)
            {
                case ArgumentException argEx:
                {
                    var error = new ApiError(400, argEx.ParamName, argEx.Message);
                    context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                    return context.Response.WriteAsJsonAsync(error);
                }
                default:
                {
                    var error = new ApiError();
                    context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    return context.Response.WriteAsJsonAsync(error);
                }
            }
        }
    }
}