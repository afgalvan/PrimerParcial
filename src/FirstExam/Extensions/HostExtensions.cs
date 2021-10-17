using System;
using System.Threading;
using Microsoft.Extensions.Hosting;
using Presentation;

namespace FirstExam.Extensions
{
    public static class HostExtensions
    {
        public static void StartHost(this IHost host)
        {
            var hostThread = new Thread(host.Start);
            hostThread.Start();
        }

        public static void StarApplication(this IHost host)
        {
            var appThread = new Thread(() => StartWpfApplication(host));
            appThread.SetApartmentState(ApartmentState.STA);
            appThread.IsBackground = true;
            appThread.Start();
        }

        private static void StartWpfApplication(IHost host)
        {
            var app = new App();
            app.InitializeComponent();
            app.Exit += (_, _) => StopHost(host);
            app.Run();
        }

        private static void StopHost(IHost host)
        {
            try
            {
                host.StopAsync().RunSynchronously();
            }
            catch (InvalidOperationException)
            {
                // ignored
            }
        }
    }
}
