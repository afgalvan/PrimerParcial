using System;
using Microsoft.Extensions.Configuration;

namespace FirstExam.Settings
{
    public static class Configuration
    {
        public static IConfiguration Startup =>
            new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddJsonFile(LoadConfigFile(), false, true)
                .Build();

        private static string GetEnvironment()
        {
#nullable enable
            string? env = Environment.GetEnvironmentVariable("ENVIRONMENT");
#nullable disable
            return env != null ? env + "." : string.Empty;
        }

        private static string LoadConfigFile()
        {
            return $"appsettings.{GetEnvironment()}json";
        }
    }
}
