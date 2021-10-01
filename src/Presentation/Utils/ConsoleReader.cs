using System;
using System.Globalization;
using Presentation.Exceptions;
using Presentation.Filters;

namespace Presentation.Utils
{
    public static class ConsoleReader
    {
        [RepeatOnException]
        public static TNumeric ReadFormattedData<TNumeric>(string question,
            Func<string, CultureInfo, TNumeric> parsing, ARange? range = null)
        {
            Console.Write(question);
            return AllowOnlyValidInput(parsing, range);
        }

        private static TNumeric AllowOnlyValidInput<TNumeric>(
            Func<string, CultureInfo, TNumeric> parsing, ARange? range)
        {
            try
            {
                TNumeric number = parsing(Console.ReadLine(), CultureInfo.CurrentUICulture);
                return GetIfWithinRange(number, range);
            }
            catch (FormatException e)
            {
                throw new InvalidUserActionException("Sólo ingrese números.", e);
            }
        }

        private static TNumeric GetIfWithinRange<TNumeric>(TNumeric number, ARange? range)
        {
            if (range != null && !range.Value.HasValue(number as IComparable))
                throw new InvalidUserActionException("Número fuera de rango.");

            return number;
        }
    }
}
