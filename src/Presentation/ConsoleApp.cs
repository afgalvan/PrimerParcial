using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Entities;
using Logic;
using Microsoft.Extensions.Hosting;
using Presentation.Exceptions;
using Presentation.Filters;
using Presentation.UIBuilder;
using Presentation.Utils;

namespace Presentation
{
    public class ConsoleApp : IHostedService
    {
        private readonly BoxBuilder     _boxBuilder;
        private readonly LodgingService _lodgingService;

        public ConsoleApp(BoxBuilder boxBuilder, LodgingService lodgingService)
        {
            _boxBuilder     = boxBuilder;
            _lodgingService = lodgingService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Menu menu = CreateMenuBuilder().Build();
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

        private MenuBuilder CreateMenuBuilder()
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

        [ExceptionPrompter]
        private async Task RegisterNewLodging(CancellationToken cancellationToken)
        {
            await _lodgingService.AddLodging(CreateLodging(), cancellationToken);
        }

        [ExceptionPrompter]
        private async Task ShowAllLodging(CancellationToken cancellationToken)
        {
            IEnumerable<Lodging> lodgings = await _lodgingService.GetAllLodging(cancellationToken);
            lodgings.ToList().ForEach(Console.WriteLine);
        }

        [ExceptionPrompter]
        private async Task DeleteOneLodging(CancellationToken cancellationToken)
        {
            Console.WriteLine("Datos originales");
            await ShowAllLodging(cancellationToken);
            Console.WriteLine("\n\nBorrar liquidación");
            int id = ConsoleReader.ReadNumericData("Ingrese el id: ", Convert.ToInt32);
            await _lodgingService.DeleteById(id, cancellationToken);
            Console.WriteLine("Datos actualizados");
            await ShowAllLodging(cancellationToken);
        }

        private Lodging CreateLodging()
        {
            string       guestType    = AskGuestType();
            RoomCapacity capacity     = AskRoomCapacity();
            int          peopleAmount = AskPeopleAmount(capacity);
            DateTime     entryDate    = AskDate("Fecha de ingreso: ");
            DateTime     exitDate     = AskDate("Fecha de salida: ");

            Lodging lodging = ManageCreation(guestType);
            lodging.RoomCapacity = capacity;
            lodging.PeopleAmount = peopleAmount;
            lodging.EntryDate    = entryDate;
            lodging.ExitDate     = exitDate;

            return lodging;
        }

        private static Lodging ManageCreation(string guestType) => guestType switch
        {
            "particular" => new GeneralLodging(),
            _ => new FellowLodging()
        };

        [RepeatOnError]
        private static string AskGuestType()
        {
            const string general = "particular";
            const string fellow  = "miembro";
            Console.Write("Tipo de huesped (particular/miembro): ");
            string guestType = Console.ReadLine()?.ToLower(CultureInfo.InvariantCulture);

            if (guestType != general && guestType != fellow)
                throw new InvalidUserActionException("Opción inválida");
            return guestType;
        }

        private RoomCapacity AskRoomCapacity()
        {
            string[] options = { "Familiar", "Sencilla", "Doble", "Suite", "", "" };

            Menu menu = new MenuBuilder(_boxBuilder)
                .WithTitle("Tipo de habitación")
                .WithOptions(options)
                .WithQuestion("Ingrese una opcion: ")
                .Build();

            int choice = menu.DisplayAndRead();
            RoomCapacity[] capacities = Enum.GetValues(typeof(RoomCapacity))
                .Cast<RoomCapacity>()
                .ToArray();

            return capacities[choice - 1];
        }

        private static int AskPeopleAmount(RoomCapacity roomCapacity)
        {
            int max   = roomCapacity.MaxCapacity();
            var range = new ARange(1, max);
            int peopleAmount = ConsoleReader.ReadNumericData($"Cantidad de huespedes (max {max}): ",
                Convert.ToInt32, range);
            return peopleAmount;
        }

        private static DateTime AskDate(string question)
        {
            return ConsoleReader.ReadNumericData(question, Convert.ToDateTime);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
