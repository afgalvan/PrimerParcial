using System;
using System.Collections.Generic;
using System.Linq;

namespace Presentation.UIBuilder
{
    public class BoxBuilder
    {
        private const int AdditionalWidth = 5;

        private int    Width      { get; set; }
        public  string ExitOption { get; set; }

        public void BoxIn(IEnumerable<string> options)
        {
            Width = LongestWordOf(options);
            PrintHorizontalRule(Width + AdditionalWidth);
            options.Select(CreateDisplayableElement).ToList().ForEach(Console.WriteLine);
            PrintHorizontalRule(Width + AdditionalWidth);
        }

        public static int LongestWordOf(IEnumerable<string> words) =>
            words.Max(word => word.Length);

        private static void PrintHorizontalRule(int width)
        {
            Console.Write('+');
            Console.Write(new string('-', width));
            Console.WriteLine('+');
        }

        public static string VoidSpaceOf(int spaceWidth) => new('\0', spaceWidth);

        private string CreateDisplayableElement(string element, int position)
        {
            string elementWithIndex = ConcatIndex(element, position);
            return CreateBoxedItem(element, elementWithIndex);
        }

        private string ConcatIndex(string element, int index)
        {
            var menuIndex                                = $"{index + 1}.";
            if (string.IsNullOrEmpty(element)) menuIndex = "  ";
            if (element == ExitOption) menuIndex         = "0.";
            return $"| {menuIndex} {element}";
        }

        private string CreateBoxedItem(string originalElement, string element)
        {
            int spaceBetween     = Width - originalElement.Length;
            var printableElement = $"{element}{VoidSpaceOf(spaceBetween)} |";
            return printableElement;
        }
    }
}
