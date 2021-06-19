using System.Threading.Tasks;

namespace Basket.Api.Application.Abstract
{
    public interface IGrpcAuthService
    {
        Task<string> GetToken();
    }
}