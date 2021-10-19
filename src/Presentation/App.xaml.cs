using System;
using System.Threading;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public        IServiceProvider  ServiceProvider   { get; set; }
        public static CancellationToken CancellationToken { get; }

        public App()
        {
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var mainWindow = ServiceProvider.GetService<MainWindow>();
            mainWindow?.Show();
        }
    }
}
