using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Entities;
using Logic;
using Microsoft.Extensions.Hosting;
using Presentation.Filters;
using Presentation.UIBuilder;
using Presentation.Utils;

namespace Presentation
{
    public class ConsoleApp : IHostedService
    {
        private readonly BoxBuilder              _boxBuilder;
        private readonly LodgingService          _lodgingService;
        private readonly LodgingRegistrationMenu _lodgingRegistration;

        public ConsoleApp(BoxBuilder boxBuilder, LodgingService lodgingService,
            LodgingRegistrationMenu lodgingRegistration)
        {
            _boxBuilder          = boxBuilder;
            _lodgingService      = lodgingService;
            _lodgingRegistration = lodgingRegistration;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Menu menu = CreateMainMenuBuilder().Build();
            await DisplayMenu(menu, cancellationToken);
        }

        private static async Task DisplayMenu(Menu menu, CancellationToken cancellationToken)
        {
            while (true)
            {
                await menu.DisplayAndReadAsync(cancellationToken);
                Console.Write("\nPresione cualquier tecla para volver al menu...");
                Console.ReadKey();
            }
        }

        private MenuBuilder CreateMainMenuBuilder()
        {
            IEnumerable<string>                        options = GetMenuOptions();
            IEnumerable<Func<CancellationToken, Task>> actions = GetMenuActions();
            return new MenuBuilder(_boxBuilder).WithTitle("Sol y Luna\n")
                .WithOptions(options)
                .WithAsyncActions(actions)
                .WithExitOption("Salir")
                .WithClear(always: true)
                .WithQuestion("Ingrese una opcion: ");
        }

        private static IEnumerable<string> GetMenuOptions()
        {
            return new[]
            {
                "Registra nueva liquidación", "Consultar todo", "Borrar liquidación", "", "Salir"
            };
        }

        private IEnumerable<Func<CancellationToken, Task>> GetMenuActions()
        {
            return new Func<CancellationToken, Task>[]
            {
                RegisterNewLodging, ShowAllLodging, DeleteOneLodging, Menu.PassAsync, Menu.ExitAsync
            };
        }

        [DisplayExceptionForUser]
        private async Task RegisterNewLodging(CancellationToken cancellationToken)
        {
            Lodging lodging = _lodgingRegistration.CreateLodgingFromInput();
            await _lodgingService.AddLodging(lodging, cancellationToken);
            Console.WriteLine("\n\nRegistrado.\n");
            Console.WriteLine(lodging);
        }

        [DisplayExceptionForUser]
        private async Task ShowAllLodging(CancellationToken cancellationToken)
        {
            IEnumerable<Lodging> lodgings = await _lodgingService.GetAllLodging(cancellationToken);
            lodgings.ToList().ForEach(Console.WriteLine);
        }

        [DisplayExceptionForUser]
        private async Task DeleteOneLodging(CancellationToken cancellationToken)
        {
            Console.WriteLine("Datos originales");
            await ShowAllLodging(cancellationToken);
            Console.WriteLine("\n\nBorrar liquidación");
            int id = ConsoleReader.ReadFormattedData("Ingrese el id: ", Convert.ToInt32);
            await _lodgingService.DeleteById(id, cancellationToken);
            Console.WriteLine("Datos actualizados");
            await ShowAllLodging(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
