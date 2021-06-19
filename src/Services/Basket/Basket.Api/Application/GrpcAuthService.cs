using System.Net.Http;
using System.Threading.Tasks;
using Basket.Api.Application.Abstract;
using IdentityModel.Client;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace Basket.Api.Application
{
    public class GrpcAuthService : IGrpcAuthService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _cache;

        public GrpcAuthService(IHttpClientFactory clientFactory, IConfiguration configuration, IMemoryCache cache)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
            _cache = cache;
        }

        public async Task<string> GetToken()
        {
            var tokenResponseInCache = _cache.Get<TokenResponse>("token-response");
            if (tokenResponseInCache != null)
            {
                return tokenResponseInCache.AccessToken;
            }

            var client = _clientFactory.CreateClient();

            var document = await client.GetDiscoveryDocumentAsync(_configuration["Jwt:Authority"]);
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = document.TokenEndpoint,
                ClientId = "basket_service",
                ClientSecret = "basket_service_secret",
                Scope = "grpc"
            });

            _cache.Set("token-response", tokenResponse);

            return tokenResponse.AccessToken;
        }
    }
}