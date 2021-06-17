
using Microsoft.Extensions.Hosting;
using Shared.Hosting;

namespace Basket.Api
{
    public class Program : Microservice<Startup>
    {
        public static void Main(string[] args)
        {
            Run(args);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => GetHostBuilder(args);
    }
}