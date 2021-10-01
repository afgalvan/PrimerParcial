using System.Threading.Tasks;
using FirstExam.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FirstExam
{
    internal static class Program
    {
        private static async Task Main(string[] args) =>
            await CreateHostBuilder(args).Build().StartAsync();

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(InjectDependencies);

        private static void InjectDependencies(IServiceCollection services)
        {
            services.AddDataDependencies();
            services.AddLogicDependencies();
            services.AddPresentationDependencies();
        }
    }
}
