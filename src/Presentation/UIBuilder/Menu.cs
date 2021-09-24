using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Presentation.Utils;

namespace Presentation.UIBuilder
{
    public class Menu
    {
        public string                                     Title              { get; set; }
        public string                                     Question           { get; set; }
        public IEnumerable<string>                        Options            { get; set; }
        public IEnumerable<Func<CancellationToken, Task>> AsyncActions       { get; set; }
        public IEnumerable<Action>                        Actions            { get; set; }
        public bool                                       ShouldClearConsole { get; set; }
        public bool                                       ClearEachOption    { get; set; }
        public bool                                       IsAsync            { get; set; }

        public string ExitOption
        {
            set => _boxBuilder.ExitOption = value;
        }


        private readonly BoxBuilder _boxBuilder;


        public Menu(BoxBuilder boxBuilder)
        {
            _boxBuilder  = boxBuilder;
            AsyncActions = new List<Func<CancellationToken, Task>>();
            Options      = new List<string>();
        }

        public void AddAsyncOption(string option, Func<CancellationToken, Task> action)
        {
            List<string> updatedOptions = Options.ToList();
            updatedOptions.Add(option);
            List<Func<CancellationToken, Task>> updatedActions = AsyncActions.ToList();
            updatedActions.Add(action);

            Options      = updatedOptions;
            AsyncActions = updatedActions;
        }

        public int DisplayAndRead()
        {
            DisplayMenuContent();
            return ReadUserChoice();
        }

        public async Task DisplayAndReadAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                await ExitAsync(cancellationToken);

            DisplayMenuContent();
            int choice = ReadUserChoice();
            ClearConsoleIfPossible();
            await ExecuteOptionAsync(choice, cancellationToken);
        }

        private void DisplayMenuContent()
        {
            ClearConsoleIfPossible();
            DisplayTitle();
            _boxBuilder.BoxIn(Options);
        }

        private void DisplayTitle()
        {
            int    menuWidth  = BoxBuilder.LongestWordOf(Options) + 8;
            int    space      = Math.Max((menuWidth - Title.Length) / 2, 0);
            string titleSpace = BoxBuilder.VoidSpaceOf(space);
            Console.WriteLine($"{titleSpace}{Title}");
        }

        private int ReadUserChoice()
        {
            var range = new ARange(0, Options.Count() - 2);
            return ConsoleReader.ReadNumericData(Question, Convert.ToInt32, range);
        }

        private void ClearConsoleIfPossible()
        {
            if (ClearEachOption)
            {
                Console.Clear();
                return;
            }

            if (ShouldClearConsole) Console.Clear();
        }

        private async Task ExecuteOptionAsync(int choice, CancellationToken cancellationToken)
        {
            await AsyncActions.ElementAt(GetMenuIndex(choice))(cancellationToken);
        }

        private int GetMenuIndex(int choice)
        {
            const int exit            = 0;
            int       lastOptionIndex = Options.Count();
            int       menuIndex       = (choice == exit ? lastOptionIndex : choice) - 1;
            return menuIndex;
        }

        public static Task ExitAsync(CancellationToken cancellationToken)
        {
            Process.GetCurrentProcess().Kill();
            return Task.CompletedTask;
        }

        public static Task PassAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
