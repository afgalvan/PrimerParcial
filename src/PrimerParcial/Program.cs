using System;
using System.Threading.Tasks;
using Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PrimerParcial
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var generalLodging = new GeneralLodging
            {
                EntryDate    = DateTime.Today - TimeSpan.FromDays(1),
                ExitDate     = DateTime.Today,
                PeopleAmount = 3,
                RoomCapacity = RoomCapacity.Familiar
            };

            Console.WriteLine(generalLodging.GuestType());
            Console.WriteLine(generalLodging.ComputePriceToPay());
            Console.WriteLine(generalLodging.ComputeRoomPrice());

            Console.WriteLine("\n");
            var fellowLodging = new FellowLodging
            {
                EntryDate    = DateTime.Today - TimeSpan.FromDays(1),
                ExitDate     = DateTime.Today,
                PeopleAmount = 1,
                RoomCapacity = RoomCapacity.Suite
            };

            Console.WriteLine(fellowLodging.GuestType());
            Console.WriteLine(fellowLodging.StayDays);
            Console.WriteLine(fellowLodging.ComputePriceToPay());
            Console.WriteLine(fellowLodging.ComputeRoomPrice());
        }


        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(InjectDependencies);

        private static void InjectDependencies(IServiceCollection services)
        {
            // services.AddDataDependencies();
            // services.AddLogicDependencies();
            // services.AddPresentationDependencies();
        }
    }
}
