using System;
using System.Globalization;
using System.Linq;
using Entities;
using Presentation.Exceptions;
using Presentation.Filters;
using Presentation.UIBuilder;
using Presentation.Utils;

namespace Presentation
{
    public class UserInputMenu
    {
        private readonly BoxBuilder _boxBuilder;
        private readonly string[]   _validGuestTypes = { "particular", "miembro", "premium" };

        public UserInputMenu(BoxBuilder boxBuilder)
        {
            _boxBuilder = boxBuilder;
        }

        public Lodging AskLodgingData()
        {
            string  guestType = AskGuestType();
            Lodging lodging   = LodgingFactory.CreateLodging(guestType);
            lodging.RoomCapacity = AskRoomCapacity();
            lodging.PeopleAmount = AskPeopleAmount(lodging.RoomCapacity);
            lodging.EntryDate    = AskDate("Fecha de ingreso: ");
            lodging.ExitDate     = AskDate("Fecha de salida: ");

            return lodging;
        }

        [RepeatOnError]
        private string AskGuestType()
        {
            string validGuestOptions = string.Join('/', _validGuestTypes);
            Console.Write($"Tipo de huesped ({validGuestOptions}): ");
            string guestType = Console.ReadLine()?.ToLower(CultureInfo.InvariantCulture);
            return OnlyIfValid(guestType);
        }

        private string OnlyIfValid(string guestType)
        {
            if (!_validGuestTypes.Contains(guestType))
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
    }
}
