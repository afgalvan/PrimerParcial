using FirstExam.Extensions;
using FirstExam.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FirstExam
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            IHost host = CreateHost(args);
            host.StartHost();
            host.StarApplication();
        }

        private static IHost CreateHost(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(InitDefaultStartup)
                .Build();

        private static void InitDefaultStartup(IServiceCollection services)
        {
            new Startup(Configuration.Startup).ConfigureServices(services);
        }
    }
}
