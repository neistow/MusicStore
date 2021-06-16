using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new[]
            {
                new ApiScope("default", "Provides user access"),
                new ApiScope("manage", "Provides admin access")
            };

        public static IEnumerable<Client> Clients =>
            new[]
            {
                new Client
                {
                    ClientId = "user",
                    RequireClientSecret = false,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowOfflineAccess = true,
                    AllowedScopes = {"openid", "profile", "default"}
                },
                new Client
                {
                    ClientId = "admin",
                    RequireClientSecret = false,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowOfflineAccess = true,
                    AllowedScopes = {"openid", "profile", "manage"}
                }
            };
    }
}