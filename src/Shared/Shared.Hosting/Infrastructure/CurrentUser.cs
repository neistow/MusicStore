using Microsoft.AspNetCore.Http;
using Shared.Abstractions;
using Shared.Hosting.Extensions;

namespace Shared.Hosting.Infrastructure
{
    public class CurrentUser : ICurrentUser
    {
        public string Id { get; }

        public CurrentUser(IHttpContextAccessor contextAccessor)
        {
            Id = contextAccessor.HttpContext?.User.Id();
        }
    }
}