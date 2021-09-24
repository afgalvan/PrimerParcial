using System;

namespace Presentation.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ErrorHandlingAttribute : Attribute
    {
        protected static void WriteError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {message}");
            Console.ResetColor();
        }
    }
}
