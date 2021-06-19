using System;
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
                new ApiScope("admin", "Provides admin access"),
                new ApiScope("grpc", "Provides grpc access")
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
                    AllowedScopes = {"openid", "profile", "admin"}
                },
                new Client
                {
                    ClientId = "basket_service",
                    RequireClientSecret = true,
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = {"grpc"},
                    ClientSecrets =
                    {
                        new Secret("basket_service_secret".Sha256())
                    },
                    AccessTokenLifetime = (int) TimeSpan.FromDays(30).TotalSeconds
                }
            };
    }
}