using Microsoft.Extensions.Hosting;
using Shared.Hosting;

namespace Catalog.Api
{
    public class Program : Microservice<Startup>
    {
        public static int Main(string[] args)
        {
            return Run(args);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => GetHostBuilder(args);
    }
}