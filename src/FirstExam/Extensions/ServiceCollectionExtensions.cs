using System.Globalization;
using Data;
using Data.Contracts;
using Data.Utils;
using Logic;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Presentation;
using Presentation.UIBuilder;

namespace FirstExam.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDataDependencies(this IServiceCollection services)
        {
            services.AddSingleton(_ => new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Culture          = CultureInfo.InvariantCulture,
                Formatting       = Formatting.Indented
            });
            services.AddScoped<IFileUpdater, JsonFileChannel>();
            services.AddScoped<IFileContentMapper, JsonFileChannel>();
            services.AddScoped<ILodgingRepository, JsonLodgingRepository>();
        }

        public static void AddLogicDependencies(this IServiceCollection services)
        {
            services.AddScoped<LodgingService>();
        }

        public static void AddPresentationDependencies(this IServiceCollection services)
        {
            services.AddTransient<BoxBuilder>();
            services.AddScoped<MenuBuilder>();
            services.AddHostedService<ConsoleApp>();
        }
    }
}
