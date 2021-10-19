using ArxOne.MrAdvice.Advice;
using System;
using System.Threading.Tasks;

namespace Presentation.Filters
{
    public class DisplayOnExceptionAttribute : ErrorHandlingAttribute, IMethodAsyncAdvice
    {
        public string DialogTitle { get; }

        public DisplayOnExceptionAttribute(string dialogTitle)
        {
            DialogTitle = dialogTitle;
        }

        public async Task Advise(MethodAsyncAdviceContext context)
        {
            try
            {
                await context.ProceedAsync();
            }
            catch (FormatException)
            {
                DisplayError(DialogTitle, "Los datos ingresados son inválidos");
            }
            catch (Exception e)
            {
                DisplayError(DialogTitle, e.Message);
            }
        }
    }
}
