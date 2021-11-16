using System.Data.Common;
using System.Globalization;
using Data;
using Data.Contracts;
using Data.Utils;
using Logic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Presentation;

namespace FirstExam.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureDbContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton<DbConnection>(
                new SqlConnection(configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly("FirstExam"))
            );
        }

        public static void AddDataDependencies(this IServiceCollection services)
        {
            services.AddSingleton(new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Culture          = CultureInfo.InvariantCulture,
                Formatting       = Formatting.Indented
            });
            services.AddScoped<IFileUpdater, JsonFileChannel>();
            services.AddScoped<IFileContentMapper, JsonFileChannel>();
            services.AddScoped<ILodgingRepository, MsSqlLodgingRepository>();
        }

        public static void AddLogicDependencies(this IServiceCollection services)
        {
            services.AddScoped<LodgingService>();
        }

        public static void AddPresentationDependencies(this IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
        }
    }
}
