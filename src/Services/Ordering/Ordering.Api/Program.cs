using Microsoft.Extensions.Hosting;
using Shared.Hosting;

namespace Ordering.Api
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