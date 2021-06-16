using System.Security.Claims;
using IdentityModel;

namespace Shared.Hosting.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string Id(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier) ?? claimsPrincipal.FindFirstValue(JwtClaimTypes.Subject);
        }
    }
}