using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Shared.Hosting.Infrastructure
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ApiControllerBase : ControllerBase
    {
        protected string CurrentUserId => User?.FindFirstValue(ClaimTypes.NameIdentifier) ??
                                          User?.FindFirstValue(JwtClaimTypes.Subject);
    }
}