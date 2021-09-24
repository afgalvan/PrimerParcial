using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dawn;

namespace Presentation.UIBuilder
{
    public class MenuBuilder
    {
        private const string DefaultTitle      = "Menu";
        private const string DefaultExitOption = "Salir";

        private Menu Menu { get; }

        public MenuBuilder(BoxBuilder boxBuilder)
        {
            Menu = new Menu(boxBuilder)
            {
                Title      = DefaultTitle,
                ExitOption = DefaultExitOption
            };
        }

        public MenuBuilder WithTitle(string title)
        {
            Menu.Title = string.IsNullOrEmpty(title) ? DefaultTitle : title;
            return this;
        }

        public MenuBuilder WithExitOption(string title)
        {
            Menu.ExitOption = string.IsNullOrEmpty(title) ? DefaultTitle : title;
            return this;
        }

        public MenuBuilder WithOptions(IEnumerable<string> options)
        {
            IEnumerable<string> safeOptions =
                Guard.Argument(options, nameof(options)).DoesNotContainNull().Value;

            Menu.Options = safeOptions;
            return this;
        }

        public MenuBuilder WithAsyncActions(IEnumerable<Func<CancellationToken, Task>> actions)
        {
            Menu.IsAsync      = true;
            Menu.AsyncActions = Guard.Argument(actions, nameof(actions)).DoesNotContainNull().Value;
            return this;
        }

        public MenuBuilder WithActions(IEnumerable<Action> actions)
        {
            Menu.Actions = Guard.Argument(actions, nameof(actions)).DoesNotContainNull().Value;
            return this;
        }

        public MenuBuilder WithClear(bool always = false)
        {
            Menu.ShouldClearConsole = true;
            Menu.ClearEachOption    = always;
            return this;
        }

        public MenuBuilder WithQuestion(string question)
        {
            Menu.Question = $"\n{question}";
            return this;
        }

        public Menu Build()
        {
            if (!Menu.Options.Any())
            {
                Menu.AddAsyncOption("", Menu.PassAsync);
                Menu.AddAsyncOption(DefaultExitOption, Menu.ExitAsync);
            }

            return Menu;
        }
    }
}
