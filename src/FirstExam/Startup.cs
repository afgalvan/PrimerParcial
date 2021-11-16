using FirstExam.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FirstExam
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureDbContext(Configuration);
            services.AddDataDependencies();
            services.AddLogicDependencies();
            services.AddPresentationDependencies();
        }
    }
}
